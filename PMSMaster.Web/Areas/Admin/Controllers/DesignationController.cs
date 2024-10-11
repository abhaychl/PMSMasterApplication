using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DesignationController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public DesignationController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}Designation/GetDesignation";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<Designation>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Designation/GetDesignationByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Designation>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Designation/GetDesignationByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Designation>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Designation Designation)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Designation/UpdateDesignation";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<Designation>(token, "Post", url, Designation);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(Designation);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Designation Designation = new Designation();
            return View(Designation);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Designation Designation)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Designation/AddDesignation";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<Designation>(token, "Post", url, Designation);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(Designation);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Designation/DeleteDesignationByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }
    }
}
