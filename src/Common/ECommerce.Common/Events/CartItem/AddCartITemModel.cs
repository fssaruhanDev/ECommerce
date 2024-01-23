using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Events.CartItem;

public class AddCartITemModel
{
    public Guid ProductId { get; set; }
    public Guid ShoppingCartId { get; set; }
    public Guid OrderID { get; set; }
    public int Quantity { get; set; }
}
