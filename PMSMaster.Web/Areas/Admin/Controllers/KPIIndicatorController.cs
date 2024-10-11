using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KPIIndicatorController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public KPIIndicatorController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}KPIIndicator/GetKPIIndicator";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<KPIIndicator>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}KPIIndicator/GetKPIIndicatorByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<KPIIndicator>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}KPIIndicator/GetKPIIndicatorByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<KPIIndicator>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(KPIIndicator KPIIndicator)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}KPIIndicator/UpdateKPIIndicator";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<KPIIndicator>(token, "Post", url, KPIIndicator);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(KPIIndicator);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            KPIIndicator KPIIndicator = new KPIIndicator();
            return View(KPIIndicator);
        }        

        [HttpPost]
        public async Task<IActionResult> Create(KPIIndicator KPIIndicator)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}KPIIndicator/AddKPIIndicator";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<KPIIndicator>(token, "Post", url, KPIIndicator);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(KPIIndicator);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}KPIIndicator/DeleteKPIIndicatorByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        private async Task GetViewBagData(string token)
        {
            var getAllRoleUrl = $"{_configuration["WebAPIUrl"]}Role/GetRole";
            var getAllKPIIndicator = $"{_configuration["WebAPIUrl"]}KPIIndicator/GetKPIIndicator";

            var allRole = await _baseHttpClient.SendRequestAsync<List<Role>>(token, "Get", getAllRoleUrl);
            var allKPIIndicator = await _baseHttpClient.SendRequestAsync<List<Role>>(token, "Get", getAllKPIIndicator);

            if (allRole.Success && allRole.Data != null)
                ViewBag.AllRoles = allRole.Data;
            
            if (allKPIIndicator.Success && allKPIIndicator.Data != null)
                ViewBag.KPIIndicator = allKPIIndicator.Data;
        }
    }
}
