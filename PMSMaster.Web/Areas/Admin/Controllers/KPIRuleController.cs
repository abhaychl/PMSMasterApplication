using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KPIRuleController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public KPIRuleController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}KPIRule/GetKPIRule";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<KPIRule>>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}KPIRule/GetKPIRuleByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<KPIRule>(token, "Get", url);

            await GetViewBagData(token);

            if (!result.Success)
                return View("Error", new ErrorViewModel { RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(KPIRule KPIRule, string clientContactPersonJson)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}KPIRule/UpdateKPIRule";
                var urlkpiRuleIndicators = $"{_configuration["WebAPIUrl"]}KPIRule/AddKPIRuleIndicator";
                var urlContactPersonDelete = $"{_configuration["WebAPIUrl"]}KPIRule/DeleteKPIRuleIndicatorID?kpiRuleId={KPIRule.KPIRuleId}";
                

                await _baseHttpClient.SendRequestAsync<Client>(token, "Get", urlContactPersonDelete);

                // Deserialize the JSON data back to a list
                List<KPIRuleIndicator> kpiRuleIndicators = JsonConvert.DeserializeObject<List<KPIRuleIndicator>>(clientContactPersonJson);

                if (kpiRuleIndicators != null && kpiRuleIndicators.Count() > 0)
                {
                    foreach (var item in kpiRuleIndicators)
                    {
                        item.KPIRuleId = KPIRule.KPIRuleId;

                        var savekpiRuleIndicators = await _baseHttpClient.SendRequestAsync<KPIRuleIndicator>(token, "Post", urlkpiRuleIndicators, item);
                    }
                }

                var result = await _baseHttpClient.SendRequestAsync<KPIRule>(token, "Post", url, KPIRule);

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

                return View(KPIRule);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            KPIRule KPIRule = new KPIRule();
            string token = Request.Cookies["AccessToken"];

            await GetViewBagData(token);

            return View(KPIRule);
        }

        [HttpPost]
        public async Task<IActionResult> Create(KPIRule KPIRule, string clientContactPersonJson)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}KPIRule/AddKPIRule";                         

                // Deserialize the JSON data back to a list
                List<KPIRuleIndicator> kpiRuleIndicators = JsonConvert.DeserializeObject<List<KPIRuleIndicator>>(clientContactPersonJson);

                if (kpiRuleIndicators != null)
                    KPIRule.KPIRuleIndicator = kpiRuleIndicators;

                var result = await _baseHttpClient.SendRequestAsync<KPIRule>(token, "Post", url, KPIRule);

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
                return View(KPIRule);
            }
        }

        [HttpGet]
        public async Task<JsonResult> IsRoleRuleExsist(int roleId)
        {
            var res = false;
            var token = Request.Cookies["AccessToken"];
            var getKPIRule = $"{_configuration["WebAPIUrl"]}KPIRule/GetKPIRule";

            var allKPIRule = await _baseHttpClient.SendRequestAsync<List<KPIRule>>(token, "Get", getKPIRule);

            if (allKPIRule.Success && allKPIRule.Data != null && allKPIRule.Data.Count > 0)
            {
                var isRuleExsist = allKPIRule.Data.FirstOrDefault(x => x.RoleId == roleId);

                res = isRuleExsist != null ? true : false;
            }

            return Json(res);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}KPIRule/DeleteKPIRuleByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        private async Task GetViewBagData(string token)
        {
            var getAllRoleUrl = $"{_configuration["WebAPIUrl"]}Role/GetRole";
            var getAllKPIIndicator = $"{_configuration["WebAPIUrl"]}KPIIndicator/GetKPIIndicator";

            var allRole = await _baseHttpClient.SendRequestAsync<List<Role>>(token, "Get", getAllRoleUrl);
            var allKPIIndicator = await _baseHttpClient.SendRequestAsync<List<KPIIndicator>>(token, "Get", getAllKPIIndicator);

            if (allRole.Success && allRole.Data != null)
                ViewBag.AllRoles = allRole.Data;
            
            if (allKPIIndicator.Success && allKPIIndicator.Data != null)
                ViewBag.AllKPIIndicator = allKPIIndicator.Data;
        }
    }
}
