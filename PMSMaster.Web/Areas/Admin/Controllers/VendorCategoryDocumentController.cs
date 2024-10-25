using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VendorCategoryDocumentController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public VendorCategoryDocumentController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}VendorCategoryDocument/GetCategoryDocument";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<VendorCategoryDocument>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}VendorCategoryDocument/GetCategoryDocumentById?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<VendorCategoryDocument>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            ViewBag.Category = await GetVendorCategory();
            var url = $"{_configuration["WebAPIUrl"]}VendorCategoryDocument/GetCategoryDocumentById?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<VendorCategoryDocument>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(VendorCategoryDocument vendorcategory)
        {
            ViewBag.Category = await GetVendorCategory();
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}VendorCategoryDocument/UpdateCategoryDocument";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<VendorCategoryDocument>(token, "Post", url, vendorcategory);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(vendorcategory);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            VendorCategoryDocument vendorCategory = new VendorCategoryDocument();
            ViewBag.Category = await GetVendorCategory();
            return View(vendorCategory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VendorCategoryDocument vendorCategory)
        {
            ViewBag.Category = await GetVendorCategory();
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}VendorCategoryDocument/AddCategoryDocument";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<VendorCategoryDocument>(token, "Post", url, vendorCategory);

                if (!result.Success)
                {
                    return View(result.Data);                    
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(vendorCategory);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}VendorCategoryDocument/DeleteCategoryDocumentByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        public async Task<List<VendorCategory>> GetVendorCategory()
        {
            var url = $"{_configuration["WebAPIUrl"]}VendorCategory/GetVendorCategory";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<VendorCategory>>(token, "Get", url);

            return !result.Success ? null : result.Data;

        }

    }
}
