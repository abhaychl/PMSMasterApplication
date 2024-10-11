using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class OfficeLocationController : ControllerBase
    {
        private readonly IOfficeLocationRepository _OfficeLocationRepository;

        public OfficeLocationController(IOfficeLocationRepository OfficeLocationRepository)
        {
            _OfficeLocationRepository = OfficeLocationRepository;
        }

        [HttpGet]
        public IActionResult GetOfficeLocation()
        {
            try
            {
                var OfficeLocation = _OfficeLocationRepository.GetOfficeLocation();
                return Ok(OfficeLocation);
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
        public IActionResult GetOfficeLocationById(int Id)
        {
            try
            {
                var OfficeLocation = _OfficeLocationRepository.GetOfficeLocationById(Id);
                return Ok(OfficeLocation);
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
        public IActionResult AddOfficeLocation(OfficeLocation OfficeLocation)
        {
            try
            {
                var addOfficeLocation = _OfficeLocationRepository.Add(OfficeLocation);
                return Ok(addOfficeLocation);
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
        public IActionResult UpdateOfficeLocation(OfficeLocation OfficeLocation)
        {
            try
            {
                var updateOfficeLocation = _OfficeLocationRepository.Update(OfficeLocation);
                return Ok(updateOfficeLocation);
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
        public IActionResult DeleteOfficeLocationByID(int Id)
        {
            try
            {
                var model = _OfficeLocationRepository.GetOfficeLocationById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _OfficeLocationRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Office Location",
                    message = ex.Message
                });
            }
        }

    }
}
