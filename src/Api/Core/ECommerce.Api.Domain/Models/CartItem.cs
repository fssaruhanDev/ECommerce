using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain.Models;

public class CartItem : BaseEntity
{
    public Guid ProductID { get; set; }
    public virtual Product Product { get; set; }

    public Guid? ShoppingCartID { get; set; }
    public virtual ShoppingCart ShoppingCart { get; set; }

    public Guid? OrderId { get; set; } 
    public virtual Order Order { get; set; }

    public int Quantity { get; set; }

}
