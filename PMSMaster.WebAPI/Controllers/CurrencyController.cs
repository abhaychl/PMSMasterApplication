using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyController(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        [HttpGet]
        public IActionResult GetCurrency()
        {
            try
            {
                var clientIndustries = _currencyRepository.GetCurrency();
                return Ok(clientIndustries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "ServerError",
                    message = ex.Message
                });
            }
        }
        
        [HttpGet]
        public IActionResult GetCurrencyById(int Id)
        {
            try
            {
                var clientIndustries = _currencyRepository.GetCurrencyById(Id);
                return Ok(clientIndustries);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    error = "InvalidInput",
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "ServerError",
                    message = ex.Message
                });
            }
        }
        
        [HttpPost]
        public IActionResult AddCurrency(Currency currency)
        {
            try
            {
                var addCurrency = _currencyRepository.Add(currency);
                return Ok(addCurrency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "ServerError",
                    message = ex.Message
                });
            }
        }
        
        [HttpPost]
        public IActionResult UpdateCurrency(Currency currency)
        {
            try
            {
                var updateCurrency = _currencyRepository.Update(currency);
                return Ok(updateCurrency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "ServerError",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult DeleteCurrencyByID(int Id)
        {
            try
            {
                var model = _currencyRepository.GetCurrencyById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _currencyRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Currency",
                    message = ex.Message
                });
            }
        }

    }
}
