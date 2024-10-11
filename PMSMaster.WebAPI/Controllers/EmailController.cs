using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class EmailController : ControllerBase
    {
        private readonly IEmailRepository _EmailRepository;

        public EmailController(IEmailRepository EmailRepository)
        {
            _EmailRepository = EmailRepository;
        }

        [HttpGet]
        public IActionResult GetEmail()
        {
            try
            {
                var Email = _EmailRepository.GetEmail();
                return Ok(Email);
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
        public IActionResult GetListEmailByCategoryId(int Id)
        {
            try
            {
                var Email = _EmailRepository.GetListEmailByCategoryId(Id);
                return Ok(Email);
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
        public IActionResult GetEmailById(int Id)
        {
            try
            {
                var Email = _EmailRepository.GetEmailById(Id);
                return Ok(Email);
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
        public IActionResult AddEmail(Email Email)
        {
            try
            {
                var addedEmail = _EmailRepository.Add(Email);
                return Ok(addedEmail);
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
        public IActionResult UpdateEmail(Email Email)
        {
            try
            {
                var updatedEmail = _EmailRepository.Update(Email);
                return Ok(updatedEmail);
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
        public IActionResult DeleteEmailByID(int Id)
        {
            try
            {
                var model = _EmailRepository.GetEmailById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _EmailRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Email",
                    message = ex.Message
                });
            }
        }

    }
}
