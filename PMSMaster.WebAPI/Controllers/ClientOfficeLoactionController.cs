using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ClientOfficeLoactionController : ControllerBase
    {
        private readonly IClientOfficeLoactionRepository _ClientOfficeLoactionRepository;

        public ClientOfficeLoactionController(IClientOfficeLoactionRepository ClientOfficeLoactionRepository)
        {
            _ClientOfficeLoactionRepository = ClientOfficeLoactionRepository;
        }

        [HttpGet]
        public IActionResult GetClientOfficeLoaction()
        {
            try
            {
                var ClientOfficeLoaction = _ClientOfficeLoactionRepository.GetClientOfficeLoaction();
                return Ok(ClientOfficeLoaction);
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
        public IActionResult GetClientOfficeLoactionById(int Id)
        {
            try
            {
                var ClientOfficeLoaction = _ClientOfficeLoactionRepository.GetClientOfficeLoactionById(Id);
                return Ok(ClientOfficeLoaction);
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
        public IActionResult AddClientOfficeLoaction(ClientOffice ClientOfficeLoaction)
        {
            try
            {
                var addClientOfficeLoaction = _ClientOfficeLoactionRepository.Add(ClientOfficeLoaction);
                return Ok(addClientOfficeLoaction);
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
        public IActionResult UpdateClientOfficeLoaction(ClientOffice ClientOfficeLoaction)
        {
            try
            {
                var updateClientOfficeLoaction = _ClientOfficeLoactionRepository.Update(ClientOfficeLoaction);
                return Ok(updateClientOfficeLoaction);
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
        public IActionResult DeleteClientOfficeLoactionByID(int Id)
        {
            try
            {
                var model = _ClientOfficeLoactionRepository.GetClientOfficeLoactionById(Id);
                
                if (model != null)
                {
                    model.IsDeleted = true;
                    _ClientOfficeLoactionRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete ClientOfficeLoaction",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public IActionResult DeleteClientOfficeLoactionByClientID(int clientID)
        {
            try
            {
                var model = _ClientOfficeLoactionRepository.GetClientOfficeLoactionByClientID(clientID);

                if (model != null)
                {
                    foreach (var item in model)
                    {
                        _ClientOfficeLoactionRepository.Delete(item);
                    }

                    return Ok();
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete ClientContactPerson",
                    message = ex.Message
                });
            }
        }

    }
}
