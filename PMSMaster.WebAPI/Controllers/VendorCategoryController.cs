using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class VendorCategoryController : ControllerBase
    {
        private readonly IVendorCategoryRepository _vendorCategoryRepository;

        public VendorCategoryController(IVendorCategoryRepository vendorCategoryRepository)
        {
            this._vendorCategoryRepository = vendorCategoryRepository;
        }

        [HttpGet]
        public IActionResult GetVendorCategory()
        {
            try
            {
                var vendorTypes = _vendorCategoryRepository.GetVendorCategory();
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
        public IActionResult GetVendorCategoryById(int Id)
        {
            try
            {
                var vendorType = _vendorCategoryRepository.GetVendorCategoryById(Id);
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
        public IActionResult AddVendorCategory(VendorCategory vendorCategory)
        {
            try
            {
                var vendor = _vendorCategoryRepository.Add(vendorCategory);
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
        public IActionResult UpdateVendorCategory(VendorCategory vendorCategory)
        {
            try
            {
                var updatevendor = _vendorCategoryRepository.Update(vendorCategory);
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
        public IActionResult DeleteVendorCategoryByID(int Id)
        {
            try
            {
                var model = _vendorCategoryRepository.GetVendorCategoryById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _vendorCategoryRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Vendor Category",
                    message = ex.Message
                });
            }
        }

    }
}
