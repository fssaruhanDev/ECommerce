namespace ECommerce.Web.Models.Order;

public class GetOrdersViewModel
{
    public Guid ShoppingCartID { get; set; }
    public double TotalPrice { get; set; }
    public List<OrderCartItems> Items { get; set; }
}
