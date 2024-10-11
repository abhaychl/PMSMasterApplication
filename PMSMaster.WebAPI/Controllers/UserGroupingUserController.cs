using PMSMaster.Entity.Models;
using PMSMaster.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PMSMaster.WebAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    //[Authorize]
    public class UserGroupingUserController : ControllerBase
    {
        private readonly IUserGroupingUsersRepository _UserGroupingUsersRepository;

        public UserGroupingUserController(IUserGroupingUsersRepository userGroupingUsersRepository)
        {
            _UserGroupingUsersRepository = userGroupingUsersRepository;

        }

         [HttpPost]
        public IActionResult AddUserGroupingUsers(List<UserGroupingUsers> userGroupingUsers)
        {
            try
            {
                var addedUserGrouping = _UserGroupingUsersRepository.AddList(userGroupingUsers);
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

        [HttpGet]
        public IActionResult DeleteUserGroupingrUsersByGroupId(int Id, int userId)
        {
            try
            {
                var model = _UserGroupingUsersRepository.GetUserGroupingUsersById(Id, userId);

                if (model != null)
                {
                    model.IsDeleted = true;
                    _UserGroupingUsersRepository.Update(model);

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
