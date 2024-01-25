namespace ECommerce.Web.Models.Order;

public class OrderCartItems
{
    public Guid Id { get; set; }
    public Guid OrderID { get; set; }
    public string Picture { get; set; }
    public double Price { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public string Size { get; set; }
    public double TotalPrice { get; set; }
}
