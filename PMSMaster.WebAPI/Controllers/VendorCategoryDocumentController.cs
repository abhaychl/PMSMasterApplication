using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class VendorCategoryDocumentController : ControllerBase
    {
        private readonly IVendorCategoryDocumentRepository _categoryDocumentRepository;

        public VendorCategoryDocumentController(IVendorCategoryDocumentRepository categoryDocumentRepository)
        {
            this._categoryDocumentRepository = categoryDocumentRepository;
        }

        [HttpGet]
        public IActionResult GetCategoryDocument()
        {
            try
            {
                var vendorTypes = _categoryDocumentRepository.GetCategoryDocument();
                return Ok(vendorTypes);
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
        public IActionResult GetCategoryDocumentById(int Id)
        {
            try
            {
                var vendorType = _categoryDocumentRepository.GetCategoryDocumentById(Id);
                return Ok(vendorType);
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
        public IActionResult AddCategoryDocument(VendorCategoryDocument categoryDocument)
        {
            try
            {
                var vendor = _categoryDocumentRepository.Add(categoryDocument);
                return Ok(vendor);
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
        public IActionResult UpdateCategoryDocument(VendorCategoryDocument categoryDocument)
        {
            try
            {
                var updatevendor = _categoryDocumentRepository.Update(categoryDocument);
                return Ok(updatevendor);
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
        public IActionResult DeleteCategoryDocumentByID(int Id)
        {
            try
            {
                var model = _categoryDocumentRepository.GetCategoryDocumentById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _categoryDocumentRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete category document",
                    message = ex.Message
                });
            }
        }

    }
}
