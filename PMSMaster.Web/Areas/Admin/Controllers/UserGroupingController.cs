using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;
using NuGet.Common;
using System.Linq;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserGroupingController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public UserGroupingController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}UserGrouping/GetUserGrouping";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<UserGrouping>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}UserGrouping/GetUserGroupingByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<UserGrouping>(token, "Get", url);


            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });


            result.Data.UserGroupingUsers.RemoveAll(x => x.IsDeleted);

            //var selectUsers = result.Data.UserGroupingUsers.Where(x => x.IsDeleted == false).Select(x => x.User).ToList();
            var selectUsers = result.Data.UserGroupingUsers.Select(x => x.User).ToList();

            await GetViewBagData(token, result.Data.UserId, selectUsers);

            //ViewBag.AllUsersWithoutGroup = result.Data.UserGroupingUsers.Select(x=>x.User);

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByRoleID(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Users/GetUsersByRoleID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<Users>>(token, "Get", url);

            //await GetViewBagData(token);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            url = $"{_configuration["WebAPIUrl"]}UserGrouping/GetUserGrouping";

            if(result.Data != null && result.Data.Count > 0)
            {
                var userGroupingResult = await _baseHttpClient.SendRequestAsync<List<UserGrouping>>(token, "Get", url);

                if (userGroupingResult.Success && userGroupingResult.Data != null && userGroupingResult.Data.Count > 0)
                {
                    foreach (var item in userGroupingResult.Data)
                    {
                        var dataFound = result.Data.FirstOrDefault(x => x.UserId == item.UserId);

                        if (dataFound != null)
                            result.Data.Remove(dataFound);
                    }
                }
            }


            return Json(result.Data);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetUsersWithoutGroup(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}Users/GetUsersWithoutGroup?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<Users>>(token, "Get", url);

            //await GetViewBagData(token);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return Json(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(UserGrouping UserGrouping, int[] UserGroupingUsers)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}UserGrouping/UpdateUserGrouping";
                var urlUserAddInGroup = $"{_configuration["WebAPIUrl"]}UserGroupingUser/AddUserGroupingUsers";
                var urlDeleteUserGroup = $"{_configuration["WebAPIUrl"]}UserGroupingUser/DeleteUserGroupingrUsersByGroupId?Id={UserGrouping.UserGroupingId}";
                var urlGetUserGroup = $"{_configuration["WebAPIUrl"]}UserGrouping/GetUserGroupingByID?Id={UserGrouping.UserGroupingId}";

                //UserGrouping.UserGroupingUsers = null;

                var userGroupList = UserGroupingUsers.ToList();

                // Get Old Data

                var resultOldData = await _baseHttpClient.SendRequestAsync<UserGrouping>(token, "Get", urlGetUserGroup);

                var filterNewUserList = new List<int>();
                var filterRemoveUserList = new List<int>();

                var UserGroupingUserList = new List<UserGroupingUsers>();

                if (resultOldData.Success && resultOldData.Data.UserGroupingUsers != null && resultOldData.Data.UserGroupingUsers.Count > 0)
                {                         
                    foreach(var item in resultOldData.Data.UserGroupingUsers)
                    {
                        if (item.IsDeleted)
                            continue;

                        var isUser = userGroupList.FirstOrDefault(x => x.Equals(item.UserId));

                        if (isUser == null || isUser < 1) // add new User is isUser is null
                        {
                            filterRemoveUserList.Add(item.UserId);
                        }
                    }

                    foreach (var item in userGroupList)
                    {
                        var isUser = resultOldData.Data.UserGroupingUsers.FirstOrDefault(x => x.UserId.Equals(item) && x.IsDeleted == false);

                        if (isUser == null) // add new User is isUser is null
                        {
                            filterNewUserList.Add(item);
                        }
                    }
                }

                if (resultOldData.Success == false || resultOldData.Data.UserGroupingUsers == null || resultOldData.Data.UserGroupingUsers.Count < 1)
                {   
                    foreach(var item in userGroupList)
                    {
                        filterNewUserList.Add(item);
                    }
                }

                if (filterRemoveUserList.Count == 0 && filterNewUserList.Count == 0)
                    return RedirectToAction("Index");

                if(filterRemoveUserList.Count > 0)
                {
                    foreach(var item in filterRemoveUserList)
                    {
                        urlDeleteUserGroup = $"{_configuration["WebAPIUrl"]}UserGroupingUser/DeleteUserGroupingrUsersByGroupId?Id={UserGrouping.UserGroupingId}&userId={item}";

                        var deleteUserGroup = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", urlDeleteUserGroup);
                    }
                    
                }                               

                if (filterNewUserList.Count > 0)
                {
                    foreach (var userId in filterNewUserList)
                    {
                        UserGroupingUserList.Add(new UserGroupingUsers
                        {
                            UserId = userId,
                            UserGroupingId = UserGrouping.UserGroupingId
                        });
                    }

                    var resultdata = await _baseHttpClient.SendRequestAsync<List<UserGroupingUsers>>(token, "Post", urlUserAddInGroup, UserGroupingUserList);
                }

                return RedirectToAction("Index");
            }
            else
            {
                await GetViewBagData(token);

                return View(UserGrouping);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            UserGrouping UserGrouping = new UserGrouping();
            string token = Request.Cookies["AccessToken"];

            await GetViewBagData(token);

            return View(UserGrouping);
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserGrouping UserGrouping, int[] UserGroupingUsers)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}UserGrouping/AddUserGrouping";

                if (UserGroupingUsers != null)
                {
                    foreach (var userId in UserGroupingUsers)
                    {
                        UserGrouping.UserGroupingUsers.Add(new UserGroupingUsers
                        {
                            UserId = userId
                        });
                    }
                }

                var result = await _baseHttpClient.SendRequestAsync<UserGrouping>(token, "Post", url, UserGrouping);

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
                return View(UserGrouping);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}UserGrouping/DeleteUserGroupingrByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        private async Task GetViewBagData(string token,int userId = 0, List<Users> preSelectedUsers = null)
        {
            var getAllRoleUrl = $"{_configuration["WebAPIUrl"]}Role/GetRole";
            var getUserurl = $"{_configuration["WebAPIUrl"]}Users/Getusers";
            var getUsersWithoutGroupurl = $"{_configuration["WebAPIUrl"]}Users/GetUsersWithoutGroup?Id={userId}";

            var allRole = await _baseHttpClient.SendRequestAsync<List<Role>>(token, "Get", getAllRoleUrl);
            var allUsers = await _baseHttpClient.SendRequestAsync<List<Users>>(token, "Get", getUserurl);
            var allUsersWithoutGroup = await _baseHttpClient.SendRequestAsync<List<Users>>(token, "Get", getUsersWithoutGroupurl);

            if (allRole.Success && allRole.Data != null)
                ViewBag.AllRoles = allRole.Data;

            if (allUsers.Success && allUsers.Data != null)
            {
                //allUsers.Data.ForEach(x => x.Name = $"{x.Name} - {x.Role?.Name}");

                ViewBag.AllUsers = allUsers.Data;
            }
            
            if (allUsersWithoutGroup.Success && allUsersWithoutGroup.Data != null)
            {
                //allUsersWithoutGroup.Data.ForEach(x => x.Name = $"{x.Name} - {x.Role?.Name}");                

                if(preSelectedUsers != null)
                    allUsersWithoutGroup.Data = allUsersWithoutGroup.Data.Concat(preSelectedUsers).ToList();

                ViewBag.AllUsersWithoutGroup = allUsersWithoutGroup.Data;
            }
        }
    }
}
