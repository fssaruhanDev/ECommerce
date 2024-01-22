using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain.Models;

public class BasketProduct : BaseEntity
{
    public int ProductQuantity { get; set; }
    public double ProductPrice { get; set; }

    public Guid BasketID { get; set; }
    public virtual Basket Basket { get; set; }

    public Guid ProductID { get; set; }
    public virtual Product Product { get; set; }
}
