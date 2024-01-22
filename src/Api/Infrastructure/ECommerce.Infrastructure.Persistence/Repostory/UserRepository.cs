using System;
using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Api.Domain.Models;
using ECommerce.Infrastructure.Persistence.Context;
using ECommerce.Infrastructure.Persistence.Repostory;

namespace ECommerce.Infrastructure.Persistence.Repository;

public class UserRepository : GenericRepository<User>,IUserRepository
{
    public UserRepository(EntityContext dbContext) : base(dbContext)
    {
    }


}

