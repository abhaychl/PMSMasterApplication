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
    public class FinancialYearController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public FinancialYearController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}FinancialYear/GetFinancialYear";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<FinancialYear>>(token,"Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return View(result.Data);
        }
        
        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}FinancialYear/GetFinancialYearByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<FinancialYear>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}FinancialYear/GetFinancialYearByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<FinancialYear>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(FinancialYear FinancialYear)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}FinancialYear/UpdateFinancialYear";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<FinancialYear>(token, "Post", url, FinancialYear);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(FinancialYear);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            FinancialYear FinancialYear = new FinancialYear();
            return View(FinancialYear);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FinancialYear FinancialYear)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}FinancialYear/AddFinancialYear";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<FinancialYear>(token, "Post", url, FinancialYear);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(FinancialYear);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}FinancialYear/DeleteFinancialYearByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }
    }
}
