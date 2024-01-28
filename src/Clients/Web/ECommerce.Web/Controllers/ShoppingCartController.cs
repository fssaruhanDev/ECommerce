using ECommerce.Web.Models.ShoppingCart;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ECommerce.Web.Controllers;

[Route("cart")]
public class ShoppingCartController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7025/api");
    private readonly HttpClient _httpClient;


    public ShoppingCartController()
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
        HttpResponseMessage response = _httpClient.GetAsync(baseAddress + "/cart/getsoppingcart?userId=" + userid).Result;


        GetShoppingCartViewModel Cart = new GetShoppingCartViewModel();

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            Cart = JsonConvert.DeserializeObject<GetShoppingCartViewModel>(data);
        }
        return View(Cart);
    }


    [HttpPost]
    [Route("buy")]
    public async Task<IActionResult> PostBuy(Guid shoppingCartId)
    {
        string token = HttpContext.Session?.GetString("token") ?? "";
        if (token == "")
        {
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Login");
        }
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        HttpResponseMessage response = _httpClient.GetAsync(baseAddress + "/cart/buy?shoppingCartId=" + shoppingCartId).Result;



        return RedirectToAction("Index", "cart");


    }

    [HttpPost]
    [Route("getDelete")]
    public async Task<IActionResult> PostDelete(Guid cartItemId)

    {
        string token = HttpContext.Session?.GetString("token") ?? "";
        if (token == "")
        {
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Login");
        }
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        await _httpClient.GetAsync(baseAddress + "/cart/delete?cartItemId=" + cartItemId);


        return RedirectToAction("Index", "cart");


    }

    [HttpPost]
    [Route("upquantity")]
    public async Task<IActionResult> PostUpdateQuantity(Guid shoppingCartId)

    {
        string token = HttpContext.Session?.GetString("token") ?? "";
        if (token == "")
        {
            HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index", "Login");
        }
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        await _httpClient.GetAsync(baseAddress + "/cart/upquantity?shoppingCartId=" + shoppingCartId);


        return RedirectToAction("Index", "cart");


    }
}
