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
    public class UserController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public UserController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}Users/Getusers";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<Users>>(token,"Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return View(result.Data);
        }
        
        public async Task<IActionResult> Detail(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Users/GetUserByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<Users>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Users/GetUserByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            await GetViewBagData(token);           

            var result = await _baseHttpClient.SendRequestAsync<Users>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Users user)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Users/UpdateUser";                

                var result = await _baseHttpClient.SendRequestAsync<Users>(token, "Post", url, user);

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
                return View(user);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {            
            string token = Request.Cookies["AccessToken"];
            await GetViewBagData(token);

            Users user = new Users();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Users user)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}Users/AddUser";                

                var result = await _baseHttpClient.SendRequestAsync<Users>(token, "Post", url, user);

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
                return View(user);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Users/DeleteuserByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        private async Task GetViewBagData(string token)
        {
            var getAllDepartmentsUrl = $"{_configuration["WebAPIUrl"]}Departments/GetDepartments";
            var getAllUserVerticals = $"{_configuration["WebAPIUrl"]}UserVerticals/GetUserVerticals";
            var getAllOfficeLocation = $"{_configuration["WebAPIUrl"]}OfficeLocation/GetOfficeLocation";
            var getAllRoleUrl = $"{_configuration["WebAPIUrl"]}Role/GetRole";

            var allDepartments = await _baseHttpClient.SendRequestAsync<List<Departments>>(token, "Get", getAllDepartmentsUrl);
            var allUserVerticals = await _baseHttpClient.SendRequestAsync<List<UserVerticals>>(token, "Get", getAllUserVerticals);
            var allOfficeLocation = await _baseHttpClient.SendRequestAsync<List<OfficeLocation>>(token, "Get", getAllOfficeLocation);
            var allRoles = await _baseHttpClient.SendRequestAsync<List<Role>>(token, "Get", getAllRoleUrl);

            if (allDepartments.Success && allDepartments.Data != null)
                ViewBag.AllDepartments = allDepartments.Data;

            if (allUserVerticals.Success && allUserVerticals.Data != null)
                ViewBag.AllUserVerticals = allUserVerticals.Data;

            if (allOfficeLocation.Success && allOfficeLocation.Data != null)
                ViewBag.AllOfficeLocation = allOfficeLocation.Data;

            if (allRoles.Success && allRoles.Data != null)
                ViewBag.AllRole = allRoles.Data;
        }
    }
}
