using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class UserGroupingController : ControllerBase
    {
        private readonly IUserGroupingRepository _UserGroupingRepository;
        private readonly IUserGroupingUsersRepository _UserGroupingUsersRepository;

        public UserGroupingController(IUserGroupingRepository UserGroupingRepository, IUserGroupingUsersRepository userGroupingUsersRepository)
        {
            _UserGroupingRepository = UserGroupingRepository;
            _UserGroupingUsersRepository = userGroupingUsersRepository;

        }

        [HttpGet]
        public IActionResult GetUserGrouping()
        {
            try
            {
                var UserGrouping = _UserGroupingRepository.GetUserGrouping();
                return Ok(UserGrouping);
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
        public IActionResult GetUserGroupingById(int Id)
        {
            try
            {
                var UserGrouping = _UserGroupingRepository.GetUserGroupingById(Id);
                return Ok(UserGrouping);
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
        
        [HttpGet]
        public IActionResult GetUserGroupingByUserId(int Id)
        {
            try
            {
                var UserGrouping = _UserGroupingRepository.GetUserGroupingByUserId(Id);
                return Ok(UserGrouping);
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
        public IActionResult AddUserGrouping(UserGrouping UserGrouping)
        {
            try
            {
                var addedUserGrouping = _UserGroupingRepository.Add(UserGrouping);
                return Ok(addedUserGrouping);
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
        public IActionResult UpdateUserGrouping(UserGrouping UserGrouping)
        {
            try
            {
                var updatedUserGrouping = _UserGroupingRepository.Update(UserGrouping);
                return Ok(updatedUserGrouping);
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
        public IActionResult DeleteUserGroupingrByID(int Id)
        {
            try
            {
                var model = _UserGroupingRepository.GetUserGroupingById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _UserGroupingRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete UserGrouping",
                    message = ex.Message
                });
            }
        }

    }
}
