using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class FaqController : ControllerBase
    {
        private readonly IFaqRepository _FaqRepository;

        public FaqController(IFaqRepository FaqRepository)
        {
            _FaqRepository = FaqRepository;
        }

        [HttpGet]
        public IActionResult GetFaq()
        {
            try
            {
                var Faq = _FaqRepository.GetFaq();
                return Ok(Faq);
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
        public IActionResult GetListFaqByCategoryId(int Id)
        {
            try
            {
                var Faq = _FaqRepository.GetListFaqByCategoryId(Id);
                return Ok(Faq);
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
        public IActionResult GetFaqById(int Id)
        {
            try
            {
                var Faq = _FaqRepository.GetFaqById(Id);
                return Ok(Faq);
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
        public IActionResult AddFaq(Faq Faq)
        {
            try
            {
                var addedFaq = _FaqRepository.Add(Faq);
                return Ok(addedFaq);
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
        public IActionResult UpdateFaq(Faq Faq)
        {
            try
            {
                var updatedFaq = _FaqRepository.Update(Faq);
                return Ok(updatedFaq);
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
        public IActionResult DeleteFaqByID(int Id)
        {
            try
            {
                var model = _FaqRepository.GetFaqById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _FaqRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Faq",
                    message = ex.Message
                });
            }
        }

    }
}
