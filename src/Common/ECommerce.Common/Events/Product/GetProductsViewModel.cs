using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Events.Product;

public class GetProductsViewModel
{

    public string Picture { get; set; }
    public string Name { get; set; }
    public Guid ID { get; set; }

}
