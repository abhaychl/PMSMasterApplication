using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class KPIIndicatorController : ControllerBase
    {
        private readonly IKPIIndicatorRepository _KPIIndicatorRepository;

        public KPIIndicatorController(IKPIIndicatorRepository KPIIndicatorRepository)
        {
            _KPIIndicatorRepository = KPIIndicatorRepository;
        }

        [HttpGet]
        public IActionResult GetKPIIndicator()
        {
            try
            {
                var KPIIndicator = _KPIIndicatorRepository.GetKPIIndicator();
                return Ok(KPIIndicator);
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
        public IActionResult GetKPIIndicatorById(int Id)
        {
            try
            {
                var KPIIndicator = _KPIIndicatorRepository.GetKPIIndicatorById(Id);
                return Ok(KPIIndicator);
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
        public IActionResult AddKPIIndicator(KPIIndicator KPIIndicator)
        {
            try
            {
                var addedKPIIndicator = _KPIIndicatorRepository.Add(KPIIndicator);
                return Ok(addedKPIIndicator);
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
        public IActionResult UpdateKPIIndicator(KPIIndicator KPIIndicator)
        {
            try
            {
                var updatedKPIIndicator = _KPIIndicatorRepository.Update(KPIIndicator);
                return Ok(updatedKPIIndicator);
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
        public IActionResult DeleteKPIIndicatorByID(int Id)
        {
            try
            {
                var model = _KPIIndicatorRepository.GetKPIIndicatorById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _KPIIndicatorRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete KPIIndicator",
                    message = ex.Message
                });
            }
        }

    }
}
