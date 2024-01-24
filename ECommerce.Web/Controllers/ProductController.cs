using ECommerce.Web.Models.Product;
using ECommerce.Web.Models.ShoppingCart;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ECommerce.Web.Controllers;

[Route("product")]
public class ProductController : Controller
{
    Uri baseAddress = new Uri("https://localhost:7025/api");
    private readonly HttpClient _httpClient;
    public ProductController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = baseAddress;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var token = HttpContext.Session.GetString("token");
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        HttpResponseMessage response = _httpClient.GetAsync(baseAddress + "/product/getproducts").Result;

        List<GetProductsViewModel> productList = new();
        if (response != null)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            productList = JsonConvert.DeserializeObject<List<GetProductsViewModel>>(data);
        }

        return View(productList);
    }



    [Route("detail/{id}")]
    public IActionResult Detail(string id)
    {
        var token = HttpContext.Session.GetString("token");
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        HttpResponseMessage response = _httpClient.GetAsync(baseAddress + "/product/detail?productID=" + id).Result;

        GetProductViewModel product = new();
        if (response != null)
        {
            var data = response.Content.ReadAsStringAsync().Result;
            product = JsonConvert.DeserializeObject<GetProductViewModel>(data);
        }

        return View(product);
    }


    [HttpPost]
    [Route("detail/{id}")]
    public async Task<IActionResult> Detail(int ProductQuantity, string id)
    {

        AddCartViewModel addCart = new AddCartViewModel();
        var userid = HttpContext.Session.GetString("id");
        var token = HttpContext.Session.GetString("token");
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        addCart.UserId = Guid.Parse(userid);
        addCart.ProductId = Guid.Parse(id);
        addCart.Quantity = ProductQuantity;
        var jsonContent = new StringContent(JsonConvert.SerializeObject(addCart), Encoding.UTF8, "application/json");

        HttpResponseMessage response = _httpClient.PostAsync(baseAddress + "/basket/addproduct", jsonContent).Result;

        if (response.IsSuccessStatusCode)
        {

            var data = response.Content.ReadAsStringAsync().Result;
            var basket = JsonConvert.DeserializeObject<Guid>(data);
            return View(basket);
        }
       return RedirectToAction("Index");
    }

}
