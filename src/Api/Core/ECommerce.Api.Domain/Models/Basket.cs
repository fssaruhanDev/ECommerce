using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain.Models;

public class Basket : BaseEntity
{
    public double TotalPrice { get; set; }
    public bool IsOrdered { get; set; }

    public Guid UserID { get; set; }
    public virtual User User { get; set; }



    public virtual ICollection<BasketProduct> BasketProducts { get; set; }
    public virtual ICollection<Order> Orders { get; set; }


}
