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
    public class FaqController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public FaqController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}Faq/GetFaq";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<Faq>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Faq/GetFaqByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Faq>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Faq/GetFaqByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            await GetViewBagData(token);

            var result = await _baseHttpClient.SendRequestAsync<Faq>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Faq Faq)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Faq/UpdateFaq";

                var result = await _baseHttpClient.SendRequestAsync<Faq>(token, "Post", url, Faq);

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
                return View(Faq);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string token = Request.Cookies["AccessToken"];
            await GetViewBagData(token);

            Faq Faq = new Faq();

            return View(Faq);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Faq Faq)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Faq/AddFaq";

                var result = await _baseHttpClient.SendRequestAsync<Faq>(token, "Post", url, Faq);

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
                return View(Faq);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Faq/DeleteFaqByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        private async Task GetViewBagData(string token)
        {
            var getAllFaqCategoryUrl = $"{_configuration["WebAPIUrl"]}FaqCategory/GetFaqCategory";

            var allFaqCategory = await _baseHttpClient.SendRequestAsync<List<FaqCategory>>(token, "Get", getAllFaqCategoryUrl);

            if (allFaqCategory.Success && allFaqCategory.Data != null)
                ViewBag.AllFaqCategorys = allFaqCategory.Data;
        }
    }
}
