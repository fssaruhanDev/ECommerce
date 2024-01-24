using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Web.Controllers;

public class ShoppingCartController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7025/api");
    private readonly HttpClient _httpClient;


    public ShoppingCartController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = baseAddress;
    }



    public IActionResult Index()
    {
        return View();
    }
}
