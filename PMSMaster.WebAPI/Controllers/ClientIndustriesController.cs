using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ClientIndustriesController : ControllerBase
    {
        private readonly IClientIndustriesRepository _clientIndustriesRepository;

        public ClientIndustriesController(IClientIndustriesRepository clientIndustriesRepository)
        {
            _clientIndustriesRepository = clientIndustriesRepository;
        }

        [HttpGet]
        public IActionResult GetClientIndustries()
        {
            try
            {
                var clientIndustries = _clientIndustriesRepository.GetClientIndustries();
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
        public IActionResult GetClientIndustriesById(int Id)
        {
            try
            {
                var clientIndustries = _clientIndustriesRepository.GetClientIndustriesById(Id);
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
        public IActionResult AddClientIndustries(ClientIndustries clientIndustries)
        {
            try
            {
                var addClientIndustries = _clientIndustriesRepository.Add(clientIndustries);
                return Ok(addClientIndustries);
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
        public IActionResult UpdateClientIndustries(ClientIndustries clientIndustries)
        {
            try
            {
                var updateClientIndustries = _clientIndustriesRepository.Update(clientIndustries);
                return Ok(updateClientIndustries);
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
        public IActionResult DeleteClientIndustriesByID(int Id)
        {
            try
            {
                var model = _clientIndustriesRepository.GetClientIndustriesById(Id);
                
                if (model != null)
                {
                    model.IsDeleted = true;
                    _clientIndustriesRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete ClientIndustries",
                    message = ex.Message
                });
            }
        }

    }
}
