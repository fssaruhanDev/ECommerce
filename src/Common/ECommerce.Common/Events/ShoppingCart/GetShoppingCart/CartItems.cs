﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Events.ShoppingCart.GetShoppingCart;

public class CartItems
{
    public Guid Id { get; set; }
    public string Picture { get; set; }
    public double Price { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Size { get; set; }
}
