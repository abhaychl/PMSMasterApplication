using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CheckListMasterController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public CheckListMasterController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}CheckListMaster/GetCheckListMaster";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<CheckListMaster>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}CheckListMaster/GetCheckListMasterByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<CheckListMaster>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}CheckListMaster/GetCheckListMasterByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<CheckListMaster>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            ViewBag.Category = await GetCheckListCategory();
            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CheckListMaster checkListMaster)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}CheckListMaster/UpdateCheckListMaster";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<CheckListMaster>(token, "Post", url, checkListMaster);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Category = await GetCheckListCategory();
                return View(checkListMaster);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CheckListMaster software = new CheckListMaster();
            ViewBag.Category = await GetCheckListCategory();
            return View(software);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CheckListMaster checkListMaster)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}CheckListMaster/AddCheckListMaster";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<CheckListMaster>(token, "Post", url, checkListMaster);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Category = await GetCheckListCategory();
                return View(checkListMaster);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}CheckListMaster/DeleteCheckListMasterByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        public async Task<List<CheckListCategory>> GetCheckListCategory()
        {
            var url = $"{_configuration["WebAPIUrl"]}ChecklistCategory/GetChecklistCategory";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<CheckListCategory>>(token, "Get", url);

            return !result.Success ? null : result.Data;

        }

    }
}
