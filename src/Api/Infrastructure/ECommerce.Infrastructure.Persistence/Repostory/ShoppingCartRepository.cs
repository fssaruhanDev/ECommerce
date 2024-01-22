using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Api.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repostory;

public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(DbContext dbContext) : base(dbContext)
    {
    }
}
