using PMSMaster.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public LoginController(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            Login model = new Login();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            var url = _configuration["APIUrl"];
            HttpResponseMessage response = await _httpClient.GetAsync($"{url}Token/Gettoken?userName={login.Username}&password={login.Password}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Token apiResponse = JsonConvert.DeserializeObject<Token>(responseBody);

                // After successful authentication
                string token = apiResponse.access_token;

                DateTimeOffset startingPoint = DateTimeOffset.Now;
                DateTimeOffset resultDateTimeOffset = startingPoint.AddSeconds(apiResponse.expires_in);

                // Store token in a secure cookie
                CookieOptions options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true, // Set to true if using HTTPS
                    //Expires = DateTime.UtcNow.AddHours(1) // Set cookie expiration time
                    Expires = resultDateTimeOffset // Set cookie expiration time
                };
                Response.Cookies.Append("AccessToken", token, options);

                // Redirect to dashboard or the desired page
                return RedirectToAction("Index", "User", new { area = "Admin" });
                //return View(apiResponse);
            }
            else
            {
                // Handle error cases
                return View("Error");
            }

            return View();
        }
    }
}
