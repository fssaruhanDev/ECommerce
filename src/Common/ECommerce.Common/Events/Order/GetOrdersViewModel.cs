using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Events.Order;

public class GetOrdersViewModel
{
    public double TotalPrice { get; set; }
    public Guid? ShoppingCartID { get; set; }
    public List<OrderCartItems> Items { get; set; }
}
