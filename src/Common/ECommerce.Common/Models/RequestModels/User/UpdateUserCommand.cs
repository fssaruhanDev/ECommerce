using System;
using MediatR;

namespace ECommerce.Common.Models.RequestModels;

public class UpdateUserCommand : IRequest<Guid>
{
    public Guid ID { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string EmailAddress { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}

