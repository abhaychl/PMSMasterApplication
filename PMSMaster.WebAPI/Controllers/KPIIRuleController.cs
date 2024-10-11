using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class KPIRuleController : ControllerBase
    {
        private readonly IKPIRuleRepository _KPIRuleRepository;
        private readonly IKPIRuleIndicatorRepository _KPIRuleIndicatorRepository;

        public KPIRuleController(IKPIRuleRepository KPIRuleRepository, IKPIRuleIndicatorRepository kPIRuleIndicatorRepository)
        {
            _KPIRuleRepository = KPIRuleRepository;
            _KPIRuleIndicatorRepository = kPIRuleIndicatorRepository;

        }

        [HttpGet]
        public IActionResult GetKPIRule()
        {
            try
            {
                var KPIRule = _KPIRuleRepository.GetKPIRule();
                return Ok(KPIRule);
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
        public IActionResult GetKPIRuleById(int Id)
        {
            try
            {
                var KPIRule = _KPIRuleRepository.GetKPIRuleById(Id);
                return Ok(KPIRule);
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
        public IActionResult GetKPIRuleByRoleId(int Id)
        {
            try
            {
                var KPIRule = _KPIRuleRepository.GetKPIRuleByRoleId(Id);
                return Ok(KPIRule);
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
        public IActionResult AddKPIRule(KPIRule KPIRule)
        {
            try
            {
                var addedKPIRule = _KPIRuleRepository.Add(KPIRule);
                return Ok(addedKPIRule);
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
        public IActionResult AddKPIRuleIndicator(KPIRuleIndicator KPIRuleIndicator)
        {
            try
            {
                var addedKPIRuleIndicator = _KPIRuleIndicatorRepository.Add(KPIRuleIndicator);
                return Ok(addedKPIRuleIndicator);
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
        public IActionResult UpdateKPIRule(KPIRule KPIRule)
        {
            try
            {
                var updatedKPIRule = _KPIRuleRepository.Update(KPIRule);
                return Ok(updatedKPIRule);
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
        public IActionResult DeleteKPIRuleByID(int Id)
        {
            try
            {
                var model = _KPIRuleRepository.GetKPIRuleById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _KPIRuleRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete KPIRule",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult DeleteKPIRuleIndicatorID(int kpiRuleId)
        {
            try
            {
                var model = _KPIRuleIndicatorRepository.DeleteKPIRuleIndicatorID(kpiRuleId);

                if (model != null)
                {
                    foreach (var item in model)
                    {
                        _KPIRuleIndicatorRepository.Delete(item);
                    }

                    return Ok();
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete OfficeContactPerson",
                    message = ex.Message
                });
            }
        }

    }
}
