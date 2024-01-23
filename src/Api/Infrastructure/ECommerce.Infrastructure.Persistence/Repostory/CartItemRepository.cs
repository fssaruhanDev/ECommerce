using ECommerce.Api.Application.Interfaces.Repostrories;
using ECommerce.Api.Domain.Models;
using ECommerce.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Infrastructure.Persistence.Repostory;

public class CartItemRepository : GenericRepository<CartItem> , ICartItemRepository
{
    public CartItemRepository(EntityContext dbContext) : base(dbContext)
    {
    }
}
