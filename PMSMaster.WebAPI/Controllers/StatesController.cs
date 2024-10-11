using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
        private readonly IStatesRepository _StatesRepository;

        public StatesController(IStatesRepository StatesRepository)
        {
            _StatesRepository = StatesRepository;
        }

        [HttpGet]
        public IActionResult GetStates()
        {
            try
            {
                var States = _StatesRepository.GetStates();
                return Ok(States);
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
        public IActionResult GetStatesByCountryId(int Id)
        {
            try
            {
                var States = _StatesRepository.GetStatesByCountryId(Id);
                return Ok(States);
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

    }
}
