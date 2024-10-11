using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using PMSMaster.Entity.Models;
using PMSMaster.Utility;
using PMSMaster.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;

namespace PMSMaster.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RateCardController : Controller
    {
        private IConfiguration _configuration;
        private BaseHttpClient _baseHttpClient;

        public RateCardController(IConfiguration configuration, BaseHttpClient baseHttpClient)
        {
            _configuration = configuration;
            _baseHttpClient = baseHttpClient;
        }
        public async Task<IActionResult> Index()
        {
            var url = $"{_configuration["WebAPIUrl"]}RateCard/GetRateCard";
            // Retrieve token from the cookie
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<List<RateCard>>(token,"Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return View(result.Data);
        }       

        private async Task<bool> CheckIsExsist(int serviceId, int sourceLangId, int targetLangId, int currencyId)
        {
            var url = $"{_configuration["WebAPIUrl"]}RateCard/GetRate?serviceId={serviceId}&sourceLangId={sourceLangId}&targetLangId={targetLangId}&currencyId={currencyId}";
            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<RateCard>(token, "Get", url);

            if (!result.Success)
                return false;

            return result.Data != null && result.Data.RateCardId > 0 ? true : false;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}RateCard/GetRateCardByID?Id={Id}";
            string token = Request.Cookies["AccessToken"];

            await GetViewBagData(token);

            var result = await _baseHttpClient.SendRequestAsync<RateCard>(token, "Get", url);

            if (!result.Success)
                return View("Error", new ErrorViewModel{ RequestId = result.ErrorMessage });

            return View(result.Data);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(RateCard RateCard)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}RateCard/UpdateRateCard";                

                var isDataExsist = await CheckIsExsist(RateCard.ServicesServiceId, RateCard.SourceLanguageId, RateCard.TargetLanguageId, RateCard.CurrencyId);

                if (isDataExsist)
                {
                    await GetViewBagData(token);
                    // If there is an error, set the error message in ViewBag
                    ViewBag.ErrorMessage = "The data already exists.";
                    return View(RateCard);
                }

                var result = await _baseHttpClient.SendRequestAsync<RateCard>(token, "Post", url, RateCard);

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
                return View(RateCard);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            string token = Request.Cookies["AccessToken"];
            await GetViewBagData(token);

            RateCard RateCard = new RateCard();

            return View(RateCard);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RateCard RateCard)
        {
            string token = Request.Cookies["AccessToken"];

            if (ModelState.IsValid)
            {
                var url = $"{_configuration["WebAPIUrl"]}RateCard/AddRateCard";                

                var isDataExsist = await CheckIsExsist(RateCard.ServicesServiceId, RateCard.SourceLanguageId, RateCard.TargetLanguageId, RateCard.CurrencyId);

                if (isDataExsist)
                {
                    await GetViewBagData(token);
                    // If there is an error, set the error message in ViewBag
                    ViewBag.ErrorMessage = "The data already exists.";
                    return View(RateCard);
                }


                var result = await _baseHttpClient.SendRequestAsync<RateCard>(token, "Post", url, RateCard);

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
                return View(RateCard);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int Id)
        {
            var url = $"{_configuration["WebAPIUrl"]}RateCard/DeleteRateCardByID?Id={Id}";

            string token = Request.Cookies["AccessToken"];

            var result = await _baseHttpClient.SendRequestAsync<bool>(token, "Get", url);

            return RedirectToAction("Index");
        }

        private async Task GetViewBagData(string token)
        {
            var getAllServicesUrl = $"{_configuration["WebAPIUrl"]}Services/GetServices";
            var getAllCurrencyUrl = $"{_configuration["WebAPIUrl"]}Currency/GetCurrency";

            var allServices = await _baseHttpClient.SendRequestAsync<List<Services>>(token, "Get", getAllServicesUrl);
            var allCurrency = await _baseHttpClient.SendRequestAsync<List<Currency>>(token, "Get", getAllCurrencyUrl);

            if (allServices.Success && allServices.Data != null)
                ViewBag.AllServices = allServices.Data;
            
            if (allCurrency.Success && allCurrency.Data != null)
                ViewBag.AllCurrency = allCurrency.Data;

            var language = Enum.GetValues(typeof(LanguageEnum))
            .Cast<LanguageEnum>()
            .Select(mode => new SelectListItem
            {
                Value = ((int)mode).ToString(),
                Text = mode.GetDisplayText() // Use the GetDisplayText method here
            });

            ViewBag.Language = new SelectList(language, "Value", "Text");
        }
    }
}
