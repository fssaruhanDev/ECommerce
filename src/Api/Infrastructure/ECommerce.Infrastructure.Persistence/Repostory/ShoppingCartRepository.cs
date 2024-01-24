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

public class ShoppingCartRepository : GenericRepository<ShoppingCart>, IShoppingCartRepository
{
    public ShoppingCartRepository(EntityContext dbContext) : base(dbContext)
    {
    }


    public async Task<ShoppingCart> FindWithIncludesShoppingCart(Guid userID)
    {
        IQueryable<ShoppingCart> query = entity;
        var shoppingCart = query.Include(i => i.User)
                                .Include(i => i.CartItems)
                                    .ThenInclude(i => i.Product)
                                .Include(i => i.CartItems)
                                    .ThenInclude(i => i.Order)
                                .FirstOrDefault(x => x.UserID == userID );


        return shoppingCart;
    }

    public async Task<ShoppingCart> FindWithIncludesShoppingCartID(Guid shoppingCartID)
    {
        IQueryable<ShoppingCart> query = entity;
        var shoppingCart = query.Include(i => i.User)
                                .Include(i => i.CartItems)
                                    .ThenInclude(i => i.Product)
                                .Include(i => i.CartItems)
                                    .ThenInclude(i => i.Order)
                                .FirstOrDefault(x => x.ID == shoppingCartID);


        return shoppingCart;
    }

}

