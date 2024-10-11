using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class SoftwareCategoryController : ControllerBase
    {
        private readonly ISoftwareCategoryRepository _softwareCategoryRepository;
        public SoftwareCategoryController(ISoftwareCategoryRepository softwareCategoryRepository)
        {
            this._softwareCategoryRepository = softwareCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetSoftwareCategory()
        {
            try
            {
                var softwareCategories = _softwareCategoryRepository.GetSoftwareCategory();
                return Ok(softwareCategories);
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
        public IActionResult GetSoftwareCategoryById(int Id)
        {
            try
            {
                var clientIndustries = _softwareCategoryRepository.GetSoftwareCategoryById(Id);
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
        public IActionResult AddSoftwareCategory(SoftwareCategory softwareCategory)
        {
            try
            {
                var softcategory = _softwareCategoryRepository.Add(softwareCategory);
                return Ok(softcategory);
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
        public IActionResult UpdateSoftwareCategory(SoftwareCategory softwareCategory)
        {
            try
            {
                var updateSoftware = _softwareCategoryRepository.Update(softwareCategory);
                return Ok(updateSoftware);
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
        public IActionResult DeleteSoftwareCategoryByID(int Id)
        {
            try
            {
                var model = _softwareCategoryRepository.GetSoftwareCategoryById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _softwareCategoryRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Software Category",
                    message = ex.Message
                });
            }
        }

    }
}
