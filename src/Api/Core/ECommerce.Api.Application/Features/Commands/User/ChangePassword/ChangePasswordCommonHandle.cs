using System;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Common.Infrastructure;
using ECommerce.Common.Models.RequestModels.User;
using ECommerce.Infrastructure.Persistence.Exeptions;
using MediatR;

namespace ECommerce.Api.Application.Features.Commands.User.ChangePassword;

public class ChangePasswordCommonHandle : IRequestHandler<ChangePasswordCommand,bool>
{

    private readonly IUserRepository userRepository;

	public ChangePasswordCommonHandle(IUserRepository userRepository)
	{
        this.userRepository = userRepository;
	}

    public async Task<bool> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (!request.ID.HasValue)
        {
            throw new ArgumentNullException(nameof(request.ID));
        }

        var dbUser = await userRepository.GetByIdAsync(request.ID.Value);

        if (dbUser is null)
            throw new DatabaseValidationException("User not found");

        var encPass = PasswordEncryptor.Encrypt(request.OldPassword);


        if(dbUser.Password != encPass)
            throw new DatabaseValidationException("Old password wrong");

        var newPass = PasswordEncryptor.Encrypt(request.NewPassword);
        dbUser.Password = newPass;

        await userRepository.UpdateAsync(dbUser);

        return true;



    }
}

