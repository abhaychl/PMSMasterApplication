using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;
using NuGet.Common;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmailController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public EmailController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}Email/GetEmail";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<Email>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Email/GetEmailByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Email>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Email/GetEmailByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            await GetViewBagData(token);

            var result = await _baseHttpClient.SendRequestAsync<Email>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Email Email)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Email/UpdateEmail";

                var result = await _baseHttpClient.SendRequestAsync<Email>(token, "Post", url, Email);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                await GetViewBagData(token);
                return View(Email);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string token = Request.Cookies["AccessToken"];
            await GetViewBagData(token);

            Email Email = new Email();

            return View(Email);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Email Email)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Email/AddEmail";

                var result = await _baseHttpClient.SendRequestAsync<Email>(token, "Post", url, Email);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                await GetViewBagData(token);
                return View(Email);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Email/DeleteEmailByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        private async Task GetViewBagData(string token)
        {
            var getAllEmailCategoryUrl = $"{_configuration["WebAPIUrl"]}EmailCategory/GetEmailCategory";

            var allEmailCategory = await _baseHttpClient.SendRequestAsync<List<Email>>(token, "Get", getAllEmailCategoryUrl);

            if (allEmailCategory.Success && allEmailCategory.Data != null)
                ViewBag.AllEmailCategorys = allEmailCategory.Data;
        }
    }
}
