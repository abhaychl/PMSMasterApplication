using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguageController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public LanguageController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}Language/GetLanguage";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<Language>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Language/GetLanguageByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Language>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Language/GetLanguageByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Language>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Language Currency)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Language/UpdateLanguage";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<Language>(token, "Post", url, Currency);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(Currency);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Language Currency = new Language();
            return View(Currency);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Language language)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Language/AddLanguage";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<Language>(token, "Post", url, language);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(language);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Language/DeleteLanguageByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

    }
}
