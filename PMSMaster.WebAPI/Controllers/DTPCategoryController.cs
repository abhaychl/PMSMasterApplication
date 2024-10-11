using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class DTPCategoryController : ControllerBase
    {
        private readonly IDTPCategoryRepository _dtpCategoryRepository;
        public DTPCategoryController(IDTPCategoryRepository dtpCategoryRepository)
        {
            this._dtpCategoryRepository = dtpCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetDTPCategory()
        {
            try
            {
                var translationTools = _dtpCategoryRepository.GetDTPCategory();
                return Ok(translationTools);
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
        public IActionResult GetDTPCategoryById(int Id)
        {
            try
            {
                var clientIndustries = _dtpCategoryRepository.GetDTPCategoryById(Id);
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
        public IActionResult AddDTPCategory(DTPCategory dTPCategory)
        {
            try
            {
                var addCurrency = _dtpCategoryRepository.Add(dTPCategory);
                return Ok(addCurrency);
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
        public IActionResult UpdateDTPCategory(DTPCategory dTPCategory)
        {
            try
            {
                var updateCurrency = _dtpCategoryRepository.Update(dTPCategory);
                return Ok(updateCurrency);
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
        public IActionResult DeleteDTPCategoryByID(int Id)
        {
            try
            {
                var model = _dtpCategoryRepository.GetDTPCategoryById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _dtpCategoryRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete DTP Category",
                    message = ex.Message
                });
            }
        }

    }
}
