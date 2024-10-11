using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class FinancialYearController : ControllerBase
    {
        private readonly IFinancialYearRepository _financialYearRepository;

        public FinancialYearController(IFinancialYearRepository FinancialYearRepository)
        {
            _financialYearRepository = FinancialYearRepository;
        }

        [HttpGet]
        public IActionResult GetFinancialYear()
        {
            try
            {
                var FinancialYear = _financialYearRepository.GetFinancialYear();
                return Ok(FinancialYear);
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
        public IActionResult GetFinancialYearById(int Id)
        {
            try
            {
                var FinancialYear = _financialYearRepository.GetFinancialYearById(Id);
                return Ok(FinancialYear);
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
        public IActionResult AddFinancialYear(FinancialYear FinancialYear)
        {
            try
            {
                var addedFinancialYear = _financialYearRepository.Add(FinancialYear);
                return Ok(addedFinancialYear);
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
        public IActionResult UpdateFinancialYear(FinancialYear FinancialYear)
        {
            try
            {
                var updatedFinancialYear = _financialYearRepository.Update(FinancialYear);
                return Ok(updatedFinancialYear);
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
        public IActionResult DeleteFinancialYearByID(int Id)
        {
            try
            {
                var model = _financialYearRepository.GetFinancialYearById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _financialYearRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete FinancialYear",
                    message = ex.Message
                });
            }
        }

    }
}
