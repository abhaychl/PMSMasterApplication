using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class UserVerticalsController : ControllerBase
    {
        private readonly IUserVerticalsRepository _UserVerticalsRepository;

        public UserVerticalsController(IUserVerticalsRepository UserVerticalsRepository)
        {
            _UserVerticalsRepository = UserVerticalsRepository;
        }

        [HttpGet]
        public IActionResult GetUserVerticals()
        {
            try
            {
                var UserVerticals = _UserVerticalsRepository.GetUserVerticals();
                return Ok(UserVerticals);
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
        public IActionResult GetUserVerticalsById(int Id)
        {
            try
            {
                var UserVerticals = _UserVerticalsRepository.GetUserVerticalsById(Id);
                return Ok(UserVerticals);
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
        public IActionResult AddUserVerticals(UserVerticals UserVerticals)
        {
            try
            {
                var addUserVerticals = _UserVerticalsRepository.Add(UserVerticals);
                return Ok(addUserVerticals);
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
        public IActionResult UpdateUserVerticals(UserVerticals UserVerticals)
        {
            try
            {
                var updateUserVerticals = _UserVerticalsRepository.Update(UserVerticals);
                return Ok(updateUserVerticals);
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
        public IActionResult DeleteUserVerticalsByID(int Id)
        {
            try
            {
                var model = _UserVerticalsRepository.GetUserVerticalsById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _UserVerticalsRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete User Verticals",
                    message = ex.Message
                });
            }
        }

    }
}
