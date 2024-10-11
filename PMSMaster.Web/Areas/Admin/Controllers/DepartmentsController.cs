using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DepartmentsController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public DepartmentsController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}Departments/GetDepartments";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<Departments>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Departments/GetDepartmentsByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Departments>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Departments/GetDepartmentsByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Departments>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Departments Departments)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Departments/UpdateDepartments";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<Departments>(token, "Post", url, Departments);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(Departments);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            Departments Departments = new Departments();
            return View(Departments);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Departments Departments)
        {
            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Departments/AddDepartments";
                string token = Request.Cookies["AccessToken"];

                var result = await _baseHttpClient.SendRequestAsync<Departments>(token, "Post", url, Departments);

                if (!result.Success)
                {
                    return View(result.Data);
                    //return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });
                }

                return RedirectToAction("Index");
            }
            else
            {
                return View(Departments);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Departments/DeleteDepartmentsByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }
    }
}
