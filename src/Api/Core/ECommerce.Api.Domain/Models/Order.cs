using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Api.Domain.Models;

public class Order : BaseEntity
{

    public bool OrderStatus { get; set; }


    public Guid BasketID { get; set; }
    public virtual Basket Basket { get; set; }

    public Guid UserID { get; set; }
    public virtual User User { get; set; }



}
