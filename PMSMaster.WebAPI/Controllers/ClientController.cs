using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository _ClientRepository;

        public ClientController(IClientRepository ClientRepository)
        {
            _ClientRepository = ClientRepository;
        }

        [HttpGet]
        public IActionResult GetClient(int assignToUser)
        {
            try
            {
                var clientIndustries = _ClientRepository.GetClient(assignToUser);
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
        public IActionResult GetClientsWithoutQuotationsCreatedInLastSixMonths(int assignToUser)
        {
            try
            {
                var clientIndustries = _ClientRepository.GetClientsWithoutQuotationsCreatedInLastSixMonths(assignToUser);
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
        public IActionResult GetAllClient()
        {
            try
            {
                var clientIndustries = _ClientRepository.GetAllClient();
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
        public IActionResult GetClientByMonthAndYear(int selectedMonth, int selectedYear)
        {
            try
            {
                var clientIndustries = _ClientRepository.GetClientByMonthAndYear(selectedMonth, selectedYear);
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
        public IActionResult GetClientById(int Id)
        {
            try
            {
                var clientIndustries = _ClientRepository.GetClientById(Id);
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
        
        [HttpGet]
        public IActionResult GetClientByLeadIdId(int Id)
        {
            try
            {
                var clientIndustries = _ClientRepository.GetClientByLeadIdId(Id);
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
        public IActionResult AddClient(Client Client)
        {
            try
            {
                var addClient = _ClientRepository.Add(Client);
                return Ok(addClient);
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
        public IActionResult UpdateClient(Client Client)
        {
            try
            {
                var updateClient = _ClientRepository.Update(Client);
                return Ok(updateClient);
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
        public IActionResult DeleteClientByID(int Id)
        {
            try
            {
                var model = _ClientRepository.GetClientById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _ClientRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Client",
                    message = ex.Message
                });
            }
        }

        [HttpGet]
        public bool ClientExistsForLead(int leadId)
        {
            return _ClientRepository.ClientExistsForLead(leadId);
        }

    }
}
