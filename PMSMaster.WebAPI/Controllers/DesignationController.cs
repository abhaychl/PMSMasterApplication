using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository _DesignationRepository;

        public DesignationController(IDesignationRepository DesignationRepository)
        {
            _DesignationRepository = DesignationRepository;
        }

        [HttpGet]
        public IActionResult GetDesignation()
        {
            try
            {
                var Designation = _DesignationRepository.GetDesignation();
                return Ok(Designation);
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
        public IActionResult GetDesignationById(int Id)
        {
            try
            {
                var Designation = _DesignationRepository.GetDesignationById(Id);
                return Ok(Designation);
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
        public IActionResult AddDesignation(Designation Designation)
        {
            try
            {
                var addedDesignation = _DesignationRepository.Add(Designation);
                return Ok(addedDesignation);
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
        public IActionResult UpdateDesignation(Designation Designation)
        {
            try
            {
                var updatedDesignation = _DesignationRepository.Update(Designation);
                return Ok(updatedDesignation);
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
        public IActionResult DeleteDesignationByID(int Id)
        {
            try
            {
                var model = _DesignationRepository.GetDesignationById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _DesignationRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Designation",
                    message = ex.Message
                });
            }
        }

    }
}
