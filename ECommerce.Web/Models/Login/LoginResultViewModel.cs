namespace ECommerce.Web.Models.Login;

public class LoginResultViewModel
{
    public Guid id { get; set; }
    public string token { get; set; }
    public string userName { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
}
