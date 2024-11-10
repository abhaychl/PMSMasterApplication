using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMSMaster.Entity.JsonModels;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class CommonMasterController : ControllerBase
    {
        private readonly ICommonRepository _commonRepository;
        public CommonMasterController(ICommonRepository commonRepository)
        {
            this._commonRepository = commonRepository;
        }

        [HttpPost]
        public async Task<IActionResult> GetMasterData(MasterDataRequest request)
        {
            try
            {
                var vendorTypes =await _commonRepository.GetMasterData(request);
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
        public async Task<IActionResult> GetStateByCountryId(int CountryId)
        {
            try
            {
                var Countries = await _commonRepository.GetStateByCountryId(CountryId);
                return Ok(Countries);
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
