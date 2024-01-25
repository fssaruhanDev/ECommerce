using ECommerce.Web.Models.Order;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.Web.Controllers;

public class OrderController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7025/api");
    private readonly HttpClient _httpClient;


    public OrderController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        string token = HttpContext.Session?.GetString("token") ?? "";
        if (token == "")
        {
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Login");
        }
        var userid = HttpContext.Session.GetString("id");
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        HttpResponseMessage response = _httpClient.GetAsync(baseAddress + "/order/get?userId="+userid).Result;

        List<GetOrdersViewModel> productList = new();
        if (response is not null)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            productList = JsonConvert.DeserializeObject<List<GetOrdersViewModel>>(data);
        }

        return View(productList);
    }
}
