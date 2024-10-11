using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ChecklistCategoryController : ControllerBase
    {
        private readonly ICheckListCategoryRepository _checklistCategoryRepository;
        public ChecklistCategoryController(ICheckListCategoryRepository checklistCategoryRepository)
        {
            this._checklistCategoryRepository = checklistCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetCheckListCategory()
        {
            try
            {
                var softwareCategories = _checklistCategoryRepository.GetCheckListCategory();
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
        public IActionResult GetCheckListCategoryById(int Id)
        {
            try
            {
                var clientIndustries = _checklistCategoryRepository.GetCheckListCategoryById(Id);
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
        public IActionResult AddCheckListCategory(CheckListCategory checkListCategory)
        {
            try
            {
                var softcategory = _checklistCategoryRepository.Add(checkListCategory);
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
        public IActionResult UpdateCheckListCategory(CheckListCategory checkListCategory)
        {
            try
            {
                var updateSoftware = _checklistCategoryRepository.Update(checkListCategory);
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
                var model = _checklistCategoryRepository.GetCheckListCategoryById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _checklistCategoryRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Checklist Category",
                    message = ex.Message
                });
            }
        }

    }
}
