using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class ChecklistMasterController : ControllerBase
    {
        private readonly ICheckListCategoryRepository _checklistCategoryRepository;
        private readonly ICheckListMasterRepository _checklistMasterRepository;
        public ChecklistMasterController(ICheckListCategoryRepository checklistCategoryRepository, ICheckListMasterRepository checklistMasterRepository)
        {
            this._checklistCategoryRepository = checklistCategoryRepository;
            this._checklistMasterRepository = checklistMasterRepository;
        }

        [HttpGet]
        public IActionResult GetChecklistMaster()
        {
            try
            {
                var softwareCategories = _checklistMasterRepository.GetCheckListMaster();
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
        public IActionResult GetChecklistMasterById(int Id)
        {
            try
            {
                var clientIndustries = _checklistMasterRepository.GetCheckListMasterById(Id);
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
        public IActionResult AddCheckListMaster(CheckListMaster checkListMaster)
        {
            try
            {
                var softcategory = _checklistMasterRepository.Add(checkListMaster);
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
        public IActionResult UpdateCheckListMaster(CheckListMaster checkListMaster)
        {
            try
            {
                var updateSoftware = _checklistMasterRepository.Update(checkListMaster);
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
        public IActionResult DeleteCheckListMasterByID(int Id)
        {
            try
            {
                var model = _checklistMasterRepository.GetCheckListMasterById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _checklistMasterRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Checklist Master",
                    message = ex.Message
                });
            }
        }

    }
}
