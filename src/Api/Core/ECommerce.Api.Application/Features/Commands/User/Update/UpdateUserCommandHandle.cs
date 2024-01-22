using System;
using AutoMapper;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Api.Domain.Models;
using ECommerce.Common;
using ECommerce.Common.Events.User;
using ECommerce.Common.Infrastructure;
using ECommerce.Common.Models.RequestModels;
using ECommerce.Infrastructure.Persistence.Exeptions;
using MediatR;

namespace ECommerce.Api.Application.Features.Commands.User.Update
{
	public class UpdateUserCommandHandle : IRequestHandler<UpdateUserCommand,Guid>
	{
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UpdateUserCommandHandle(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;

        }

        public async Task<Guid> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var userExist = await userRepository.GetByIdAsync(request.ID);
           
            if (userExist is not null)
                throw new DatabaseValidationException("User not found");

            var userEmailAddress = userExist.EmailAddress;
            var emailChanged = string.CompareOrdinal(userEmailAddress , request.EmailAddress) != 0;

            mapper.Map(request, userExist);
            var item = await userRepository.UpdateAsync(userExist);


            if (emailChanged && item > 0)
            {

                var @event = new UserEmailChangedEvent() { NewEmailAddress = userExist.EmailAddress, OldEmailAddress = null };



                QueueFactory.SendMessages(ECommerceConstant.UserExchangeName, ECommerceConstant.DefaultExchangeType, ECommerceConstant.UserEmailChangedQueueName, @event);

                userExist.EmailConfirmed = false;
                await userRepository.UpdateAsync(userExist);
            }

            return userExist.ID;


        }
    }
}

