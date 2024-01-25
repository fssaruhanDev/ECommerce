namespace ECommerce.Web.Models.Product;

public class GetProductsViewModel
{
    public string Picture { get; set; }
    public string Name { get; set; }
    public Guid ID { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
}
