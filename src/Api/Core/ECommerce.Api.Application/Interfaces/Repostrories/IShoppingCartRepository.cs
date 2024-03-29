﻿using ECommerce.Api.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Application.Interfaces.Repostrories;

public interface IShoppingCartRepository : IGenericRepository<ShoppingCart>
{
   Task<ShoppingCart> FindWithIncludesShoppingCart(Guid userID);
   Task<ShoppingCart> FindWithIncludesShoppingCartID(Guid shoppingCartID);
}
