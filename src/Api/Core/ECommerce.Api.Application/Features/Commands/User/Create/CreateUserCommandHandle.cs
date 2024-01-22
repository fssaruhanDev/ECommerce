using System;
using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common;
using ECommerce.Common.Events.User;
using ECommerce.Common.Infrastructure;
using ECommerce.Common.Models.RequestModels;
using ECommerce.Infrastructure.Persistence.Exeptions;
using MediatR;

namespace ECommerce.Api.Application.Features.Commands.User.CreateUser;

public class CreateUserCommandHandle : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public CreateUserCommandHandle(IMapper mapper, IUserRepository userRepository)
    {
        this.mapper = mapper;
        this.userRepository = userRepository;

    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userExist = await userRepository.GetSingleAsync(x => x.EmailAddress == request.EmailAddress);

        if (userExist is not null)
            throw new DatabaseValidationException("User allready exsist");


        var dbUser = mapper.Map<Domain.Models.User>(request);

        var item = await userRepository.AddAsync(dbUser);


        if (item>0)
        {

            var @event = new UserEmailChangedEvent() { NewEmailAddress = dbUser.EmailAddress, OldEmailAddress = null };



            QueueFactory.SendMessages(ECommerceConstant.UserExchangeName,ECommerceConstant.DefaultExchangeType,ECommerceConstant.UserEmailChangedQueueName,@event);
        }

        return dbUser.ID;

    }
}

