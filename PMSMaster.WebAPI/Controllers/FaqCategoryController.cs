using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class FaqCategoryController : ControllerBase
    {
        private readonly IFaqCategoryRepository _FaqCategoryRepository;

        public FaqCategoryController(IFaqCategoryRepository FaqCategoryRepository)
        {
            _FaqCategoryRepository = FaqCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetFaqCategory()
        {
            try
            {
                var FaqCategory = _FaqCategoryRepository.GetFaqCategory();
                return Ok(FaqCategory);
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
        public IActionResult GetFaqCategoryById(int Id)
        {
            try
            {
                var FaqCategory = _FaqCategoryRepository.GetFaqCategoryById(Id);
                return Ok(FaqCategory);
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
        public IActionResult AddFaqCategory(FaqCategory FaqCategory)
        {
            try
            {
                var addedFaqCategory = _FaqCategoryRepository.Add(FaqCategory);
                return Ok(addedFaqCategory);
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
        public IActionResult UpdateFaqCategory(FaqCategory FaqCategory)
        {
            try
            {
                var updatedFaqCategory = _FaqCategoryRepository.Update(FaqCategory);
                return Ok(updatedFaqCategory);
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
        public IActionResult DeleteFaqCategoryByID(int Id)
        {
            try
            {
                var model = _FaqCategoryRepository.GetFaqCategoryById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _FaqCategoryRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete FaqCategory",
                    message = ex.Message
                });
            }
        }

    }
}
