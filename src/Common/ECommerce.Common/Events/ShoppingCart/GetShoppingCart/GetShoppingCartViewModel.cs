using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Events.ShoppingCart.GetShoppingCart;

public class GetShoppingCartViewModel
{
    public Guid Id{ get; set; }
    public double TotalPrice { get; set; }
    public List<CartItems> Items { get; set; }
}
