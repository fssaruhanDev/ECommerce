using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Common.Events.Product;

public class GetProductViewModel
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Picture { get; set; }
    public string Description { get; set; }
    public string Company { get; set; }
    public string Size { get; set; }
    public string Barcode { get; set; }
    public int Stock { get; set; }
    public double Price { get; set; }
}
