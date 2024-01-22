using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain.Models;

public class ShoppingCart : BaseEntity
{

    public Guid UserID { get; set; }
    public virtual User User { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; }
}
