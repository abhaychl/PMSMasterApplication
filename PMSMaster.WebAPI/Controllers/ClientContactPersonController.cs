using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ClientContactPersonController : ControllerBase
    {
        private readonly IClientContactPersonRepository _ClientContactPersonRepository;

        public ClientContactPersonController(IClientContactPersonRepository ClientContactPersonRepository)
        {
            _ClientContactPersonRepository = ClientContactPersonRepository;
        }

        [HttpGet]
        public IActionResult GetClientContactPerson()
        {
            try
            {
                var OfficeContactPerson = _ClientContactPersonRepository.GetClientContactPerson();
                return Ok(OfficeContactPerson);
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
        public IActionResult GetClientContactPersonById(int Id)
        {
            try
            {
                var OfficeContactPerson = _ClientContactPersonRepository.GetClientContactPersonById(Id);
                return Ok(OfficeContactPerson);
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
        public IActionResult AddClientContactPerson(OfficeContactPerson OfficeContactPerson)
        {
            try
            {
                var addClientContactPerson = _ClientContactPersonRepository.Add(OfficeContactPerson);
                return Ok(addClientContactPerson);
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
        public IActionResult UpdateClientContactPerson(OfficeContactPerson OfficeContactPerson)
        {
            try
            {
                var updateClientContactPerson = _ClientContactPersonRepository.Update(OfficeContactPerson);
                return Ok(updateClientContactPerson);
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
        public IActionResult DeleteClientContactPersonByID(int Id)
        {
            try
            {
                var model = _ClientContactPersonRepository.GetClientContactPersonById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _ClientContactPersonRepository.Update(model);

                    return Ok(model?.IsDeleted);
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

        [HttpGet]
        public IActionResult DeleteClientContactPersonByClientID(int clientID)
        {
            try
            {
                var model = _ClientContactPersonRepository.GetClientContactPersonByClientID(clientID);

                if (model != null)
                {
                    foreach (var item in model)
                    {
                        _ClientContactPersonRepository.Delete(item);
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

        [HttpGet]
        public IActionResult DeleteClientContactPersonByOfficeLocationID(int officeLocationId)
        {
            try
            {
                var model = _ClientContactPersonRepository.DeleteClientContactPersonByOfficeLocationID(officeLocationId);

                if (model != null)
                {
                    foreach (var item in model)
                    {
                        _ClientContactPersonRepository.Delete(item);
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

        [HttpGet]
        public IActionResult GetClientContactPersonByClientID(int Id)
        {
            try
            {
                var OfficeContactPerson = _ClientContactPersonRepository.GetClientContactPersonByClientID(Id);
                return Ok(OfficeContactPerson);
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
        public IActionResult GetClientOfficeLocationByClientID(int Id)
        {
            try
            {
                var OfficeList = _ClientContactPersonRepository.GetClientOfficeLocationByClientID(Id);
                return Ok(OfficeList);
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
        public IActionResult GetClientContactPersonByOfficeID(int Id)
        {
            try
            {
                var OfficeContactPerson = _ClientContactPersonRepository.GetClientContactPersonByOfficeID(Id);
                return Ok(OfficeContactPerson);
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
    }
}
