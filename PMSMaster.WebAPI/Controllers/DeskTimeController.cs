using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class DeskTimeController : ControllerBase
    {
        private readonly IDeskTimeRepository _DeskTimeRepository;

        public DeskTimeController(IDeskTimeRepository DeskTimeRepository)
        {
            _DeskTimeRepository = DeskTimeRepository;
        }

        [HttpGet]
        public IActionResult GetDeskTime()
        {
            try
            {
                var DeskTime = _DeskTimeRepository.GetDeskTime();
                return Ok(DeskTime);
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
        public IActionResult GetListDeskTimeByCategoryId(int Id)
        {
            try
            {
                var DeskTime = _DeskTimeRepository.GetListDeskTimeByCategoryId(Id);
                return Ok(DeskTime);
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
        public IActionResult GetDeskTimeByMonthAndYear(int month, int year, int assignToUser)
        {
            try
            {
                var DeskTime = _DeskTimeRepository.GetDeskTimeByMonthAndYear(month , year, assignToUser);
                return Ok(DeskTime);
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
        public IActionResult GetDeskTimeById(int Id)
        {
            try
            {
                var DeskTime = _DeskTimeRepository.GetDeskTimeById(Id);
                return Ok(DeskTime);
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
        public IActionResult AddDeskTime(DeskTime DeskTime)
        {
            try
            {
                var addedDeskTime = _DeskTimeRepository.Add(DeskTime);
                return Ok(addedDeskTime);
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
        public IActionResult UpdateDeskTime(DeskTime DeskTime)
        {
            try
            {
                var updatedDeskTime = _DeskTimeRepository.Update(DeskTime);
                return Ok(updatedDeskTime);
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
        public IActionResult DeleteDeskTimeByID(int Id)
        {
            try
            {
                var model = _DeskTimeRepository.GetDeskTimeById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _DeskTimeRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete DeskTime",
                    message = ex.Message
                });
            }
        }

    }
}
