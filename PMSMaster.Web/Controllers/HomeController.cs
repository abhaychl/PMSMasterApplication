using PMSMaster.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;

namespace PMSMaster.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, HttpClient httpClient, IConfiguration configuration)
        {
            _logger = logger;
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Login()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            Login model = new Login();

            // Retrieve InfoMessage from TempData
            ViewBag.InfoMessage = TempData["InfoMessage"];

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
                
                startingPoint = DateTimeOffset.Now;
                DateTimeOffset resultDateTimeOffsetNew = startingPoint.AddSeconds(apiResponse.expires_in);


                // Store token in a secure cookie
                CookieOptions options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // Set to true if using HTTPS
                    //Expires = DateTime.UtcNow.AddHours(1) // Set cookie expiration time
                    Expires = resultDateTimeOffset // Set cookie expiration time
                };

                Response.Cookies.Append("AccessToken", token, options);
                Response.Cookies.Append("UserId", apiResponse.UserId.ToString(), options);

                options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // Set to true if using HTTPS
                    //Expires = DateTime.UtcNow.AddHours(1) // Set cookie expiration time
                    Expires = resultDateTimeOffsetNew // Set cookie expiration time
                };

                Response.Cookies.Append("UserName", login.Username, options);
                Response.Cookies.Append("UserRole", apiResponse.Role, options);
                Response.Cookies.Append("UserRoleId", apiResponse.RoleId.ToString(), options);
                Response.Cookies.Append("TargetAmount", apiResponse.Target.ToString(), options);

                // Redirect to dashboard or the desired page
                return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
                //return View(apiResponse);
            }
            else
            {
                ModelState.AddModelError("Password", "Please enter correct password");
                // Handle error cases
                return View();
            }
        }

        public IActionResult AdminLogin()
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }

            Login model = new Login();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AdminLogin(Login login)
        {
            var url = _configuration["APIUrl"];
            HttpResponseMessage response = await _httpClient.GetAsync($"{url}Token/Gettoken?userName={login.Username}&password={login.Password}");

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Token apiResponse = JsonConvert.DeserializeObject<Token>(responseBody);

                if(apiResponse == null || apiResponse.RoleId != 6) /// Check for Admin
                {
                    ModelState.AddModelError("Password", "This user is not an admin.");
                    // Handle error cases
                    return View();
                }


                // After successful authentication
                string token = apiResponse.access_token;

                DateTimeOffset startingPoint = DateTimeOffset.Now;
                DateTimeOffset resultDateTimeOffset = startingPoint.AddSeconds(apiResponse.expires_in);

                startingPoint = DateTimeOffset.Now;
                DateTimeOffset resultDateTimeOffsetNew = startingPoint.AddSeconds(apiResponse.expires_in);


                // Store token in a secure cookie
                CookieOptions options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // Set to true if using HTTPS
                    //Expires = DateTime.UtcNow.AddHours(1) // Set cookie expiration time
                    Expires = resultDateTimeOffset // Set cookie expiration time
                };

                Response.Cookies.Append("AccessToken", token, options);
                Response.Cookies.Append("UserId", apiResponse.UserId.ToString(), options);

                options = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = false, // Set to true if using HTTPS
                    //Expires = DateTime.UtcNow.AddHours(1) // Set cookie expiration time
                    Expires = resultDateTimeOffsetNew // Set cookie expiration time
                };

                Response.Cookies.Append("UserName", login.Username, options);
                Response.Cookies.Append("UserRole", apiResponse.Role, options);
                Response.Cookies.Append("UserRoleId", apiResponse.RoleId.ToString(), options);
                Response.Cookies.Append("TargetAmount", apiResponse.Target.ToString(), options);

                // Redirect to dashboard or the desired page
                return RedirectToAction("Index", "DashBoard", new { area = "Admin" });
                //return View(apiResponse);
            }
            else
            {
                ModelState.AddModelError("Password", "Please enter correct password");
                // Handle error cases
                return View();
            }
        }
    }
}