namespace ECommerce.Web.Models.ShoppingCart;

public class AddCartViewModel
{
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
