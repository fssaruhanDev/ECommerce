using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Newtonsoft.Json;
using System.Text;
using ECommerce.Web.Models.Login;


namespace ECommerce.Web.Controllers
{
    public class LoginController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7025/api");
        private readonly HttpClient _httpClient;


        public LoginController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel login)
        {

            var jsonContent = new StringContent(JsonConvert.SerializeObject(login), Encoding.UTF8, "application/json");

            HttpResponseMessage response = _httpClient.PostAsync(baseAddress + "/user/login",jsonContent).Result;

            LoginResultViewModel loginResult = new LoginResultViewModel();
            if (response.IsSuccessStatusCode)
            {

                var data = response.Content.ReadAsStringAsync().Result;
                loginResult = JsonConvert.DeserializeObject<LoginResultViewModel>(data);

                HttpContext.Session.SetString("token", loginResult.token);
                HttpContext.Session.SetString("id", loginResult.id.ToString());
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, loginResult.firstName + " "+ loginResult.lastName),
                    new Claim(ClaimTypes.NameIdentifier, loginResult.token),
                    new Claim("Role","User")
                };
                var identity = new ClaimsIdentity(claims, "CookieAuth");
                ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync("CookieAuth", principal);
                return RedirectToAction("Index", "Home");

            }
            return View();
        }

        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("Index");
        }
    }
}
