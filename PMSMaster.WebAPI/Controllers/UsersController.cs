//using PMSMaster.Data.Interface;
using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Getusers()
        {
            try
            {
                var users = _userRepository.GetUsers();
                return Ok(users);
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
        public async Task<IActionResult> GetLMSusers()
        {
            try
            {
                var users =await _userRepository.GetLMSUsers();
                return Ok(users);
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
        public IActionResult GetUserByID(int Id)
        {
            try
            {
                var user = _userRepository.GetUserById(Id);
                return Ok(user);
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
        public IActionResult GetUsersByRoleID(int Id)
        {
            try
            {
                var user = _userRepository.GetUsersByRoleID(Id);
                return Ok(user);
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
        public IActionResult GetUsersWithoutGroup(int Id)
        {
            try
            {
                var user = _userRepository.GetUsersWithoutGroup(Id);
                return Ok(user);
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
        public IActionResult AddUser(Users users)
        {
            try
            {
                var addedUser = _userRepository.Add(users);
                return Ok(addedUser);
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
        public IActionResult UpdateUser(Users users)
        {
            try
            {
                var updateduser = _userRepository.Update(users);
                return Ok(updateduser);
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
        public IActionResult DeleteuserByID(int Id)
        {
            try
            {
                var model = _userRepository.GetUserById(Id);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _userRepository.Update(model);

                    return Ok(model?.IsDeleted);
                }

                return Ok(false);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "Unable to delete user",
                    message = ex.Message
                });
            }
        }

    }
}
