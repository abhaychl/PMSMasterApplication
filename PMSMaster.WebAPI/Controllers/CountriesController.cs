using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _CountriesRepository;

        public CountriesController(ICountriesRepository CountriesRepository)
        {
            _CountriesRepository = CountriesRepository;
        }

        [HttpGet]
        public IActionResult GetCountries()
        {
            try
            {
                var Countries = _CountriesRepository.GetCountries();
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
        
        [HttpGet]
        public IActionResult GetCountriesById(int Id)
        {
            try
            {
                var Countries = _CountriesRepository.GetCountriesById(Id);
                return Ok(Countries);
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
