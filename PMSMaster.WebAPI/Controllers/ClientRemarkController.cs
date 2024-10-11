using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ClientRemarkController : ControllerBase
    {
        private readonly IClientRemarkRepository _ClientRemarkRepository;
        private readonly IClientRepository _ClientRepository;

        public ClientRemarkController(IClientRemarkRepository ClientRemarkRepository, IClientRepository ClientRepository)
        {
            _ClientRemarkRepository = ClientRemarkRepository;
            _ClientRepository = ClientRepository;

        }

        [HttpGet]
        public IActionResult GetClientRemark()
        {
            try
            {
                var ClientRemark = _ClientRemarkRepository.GetClientRemark();
                return Ok(ClientRemark);
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
        public IActionResult GetClientRemarkById(int Id)
        {
            try
            {
                var ClientRemark = _ClientRemarkRepository.GetClientRemarkById(Id);
                return Ok(ClientRemark);
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
        public IActionResult GetClientRemarkByAppointmentId(int Id)
        {
            try
            {
                var ClientRemark = _ClientRemarkRepository.GetClientRemarkByAppointmentId(Id);
                return Ok(ClientRemark);
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
        public IActionResult AddClientRemark(ClientRemark ClientRemark)
        {
            try
            {
                var addClientRemark = _ClientRemarkRepository.Add(ClientRemark);

                // udpate Client status
                if (addClientRemark != null)
                {
                    var getClient = _ClientRepository.GetClientById(ClientRemark.ClientId);
                    getClient.AssignToUser = ClientRemark.UserId;

                    _ClientRepository.Update(getClient);
                }
                // end update Client status

                return Ok(addClientRemark);
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
        public IActionResult UpdateClientRemark(ClientRemark ClientRemark)
        {
            try
            {
                var updateClientRemark = _ClientRemarkRepository.Update(ClientRemark);
                return Ok(updateClientRemark);
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
        public IActionResult DeleteClientRemarkByID(int Id)
        {
            try
            {
                var model = _ClientRemarkRepository.GetClientRemarkById(Id);
                
                if (model != null)
                {
                    model.IsDeleted = true;
                    _ClientRemarkRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete ClientRemark",
                    message = ex.Message
                });
            }
        }
    }
}
