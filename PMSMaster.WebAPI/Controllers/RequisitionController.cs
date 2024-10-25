using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class RequisitionController : ControllerBase
    {
        private readonly IRequisitionRepository _requisitionRepository;
        public RequisitionController(IRequisitionRepository requisitionRepository)
        {
            _requisitionRepository = requisitionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetRequisitionFormData()
        {
            try
            {
                var vendorTypes =await _requisitionRepository.GetRequisitionFormData();
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
        
    
    }
}
