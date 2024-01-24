namespace ECommerce.Web.Models.ShoppingCart;

public class AddCartViewModel
{
    public Guid userId { get; set; }
    public Guid productId { get; set; }
    public int quantity { get; set; }
}
