using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ServicesController : ControllerBase
    {
        private readonly IServicesRepository _servicesRepository;

        public ServicesController(IServicesRepository servicesRepository)
        {
            _servicesRepository = servicesRepository;
        }

        [HttpGet]
        public IActionResult GetServices()
        {
            try
            {
                var clientIndustries = _servicesRepository.GetServices();
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
        public IActionResult GetServicesById(int Id)
        {
            try
            {
                var clientIndustries = _servicesRepository.GetServicesById(Id);
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
        public IActionResult AddServices(Services services)
        {
            try
            {
                var addServices = _servicesRepository.Add(services);
                return Ok(addServices);
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
        public IActionResult UpdateServices(Services services)
        {
            try
            {
                var updateServices = _servicesRepository.Update(services);
                return Ok(updateServices);
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
        public IActionResult DeleteServicesByID(int Id)
        {
            try
            {
                var model = _servicesRepository.GetServicesById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _servicesRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Services",
                    message = ex.Message
                });
            }
        }
    }
}
