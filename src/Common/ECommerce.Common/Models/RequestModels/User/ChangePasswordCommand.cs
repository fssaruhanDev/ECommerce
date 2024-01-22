using System;
using MediatR;

namespace ECommerce.Common.Models.RequestModels.User
{
	public class ChangePasswordCommand : IRequest<bool>
	{
		public Guid? ID { get; set; }
		public string OldPassword { get; set; }
		public string NewPassword { get; set; }


		public ChangePasswordCommand(string oldPassword,string newPassword)
		{
			OldPassword = oldPassword;
			NewPassword = newPassword; 

		}
	}
}

