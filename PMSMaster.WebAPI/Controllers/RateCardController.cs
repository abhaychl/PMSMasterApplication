using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class RateCardController : ControllerBase
    {
        private readonly IRateCardRepository _RateCardRepository;

        public RateCardController(IRateCardRepository RateCardRepository)
        {
            _RateCardRepository = RateCardRepository;
        }

        [HttpGet]
        public IActionResult GetRateCard()
        {
            try
            {
                var RateCard = _RateCardRepository.GetRateCard();
                return Ok(RateCard);
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
        public IActionResult GetListRateCardByCategoryId(int Id)
        {
            try
            {
                var RateCard = _RateCardRepository.GetListRateCardByCategoryId(Id);
                return Ok(RateCard);
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
        public IActionResult GetRateCardById(int Id)
        {
            try
            {
                var RateCard = _RateCardRepository.GetRateCardById(Id);
                return Ok(RateCard);
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

        [HttpGet]
        public IActionResult GetRate(int serviceId, int sourceLangId, int targetLangId, int currencyId)
        {
            try
            {
                var RateCard = _RateCardRepository.GetRate(serviceId, sourceLangId, targetLangId, currencyId);
                return Ok(RateCard);
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
        public IActionResult AddRateCard(RateCard RateCard)
        {
            try
            {
                var addedRateCard = _RateCardRepository.Add(RateCard);
                return Ok(addedRateCard);
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
        public IActionResult UpdateRateCard(RateCard RateCard)
        {
            try
            {
                var updatedRateCard = _RateCardRepository.Update(RateCard);
                return Ok(updatedRateCard);
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
        public IActionResult DeleteRateCardByID(int Id)
        {
            try
            {
                var model = _RateCardRepository.GetRateCardById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _RateCardRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete RateCard",
                    message = ex.Message
                });
            }
        }

    }
}
