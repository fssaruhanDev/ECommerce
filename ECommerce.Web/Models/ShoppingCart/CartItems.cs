namespace ECommerce.Web.Models.ShoppingCart;

public class CartItems
{
    public Guid Id { get; set; }
    public string Picture { get; set; }
    public double Price { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Size { get; set; }
}
