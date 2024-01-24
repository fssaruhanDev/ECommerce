using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Events.ShoppingCart.AddShoppingCart;

public class AddCartOrder
{
    public Guid Id { get; set; }
    public Guid UserID { get; set; }
}
