using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class VendorTypeController : ControllerBase
    {
        private readonly IVendorTypeRepository _vendorTypeRepository;

        public VendorTypeController(IVendorTypeRepository vendorTypeRepository)
        {
            _vendorTypeRepository = vendorTypeRepository;
        }

        [HttpGet]
        public IActionResult GetVendorTypes()
        {
            try
            {
                var vendorTypes = _vendorTypeRepository.GetVendorTypes();
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
        public IActionResult GetVendorTypeById(int Id)
        {
            try
            {
                var vendorType = _vendorTypeRepository.GetVendorTypeById(Id);
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
        public IActionResult AddVendorType(VendorType vendorType)
        {
            try
            {
                var vendor = _vendorTypeRepository.Add(vendorType);
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
        public IActionResult UpdateVendorType(VendorType vendorType)
        {
            try
            {
                var updatevendor = _vendorTypeRepository.Update(vendorType);
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
        public IActionResult DeleteVendorTypeByID(int Id)
        {
            try
            {
                var model = _vendorTypeRepository.GetVendorTypeById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _vendorTypeRepository.Update(model);
                   
                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete Vendor Type",
                    message = ex.Message
                });
            }
        }

    }
}
