namespace ECommerce.Web.Models.ShoppingCart;

public class GetShoppingCartViewModel
{
    public Guid Id { get; set; }
    public double TotalPrice { get; set; }
    public List<CartItems> Items { get; set; }
}
