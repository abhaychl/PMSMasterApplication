//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IUserGroupingUsersRepository : IGenericAsyncRepository<UserGroupingUsers>
    {
        List<UserGroupingUsers> GetUserGroupingUsers();
        UserGroupingUsers GetUserGroupingUsersById(int id, int userId);
        UserGroupingUsers GetUserGroupingUsersByUserGroupingUsersIdPasswor(string loginId, string password);

    }
    public class UserGroupingUsersRepository : GenericAsyncRepository<UserGroupingUsers>, IUserGroupingUsersRepository
    {
        public UserGroupingUsersRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<UserGroupingUsers> GetUserGroupingUsers()
        {
            var lstUserGroupingUsers = _dbContext.UserGroupingUsers.Where(x => x.IsDeleted == false).ToList();
            return lstUserGroupingUsers;
        }        

        public UserGroupingUsers GetUserGroupingUsersById(int id, int userId)
        {
            var UserGroupingUsers = _dbContext.UserGroupingUsers.FirstOrDefault(x => x.UserGroupingId == id && x.UserId == userId && x.IsDeleted == false);
            return UserGroupingUsers;
        }

        public UserGroupingUsers GetUserGroupingUsersByUserGroupingUsersIdPasswor(string loginId, string password)
        {
            var UserGroupingUsers = _dbContext.UserGroupingUsers.FirstOrDefault(x => x.Status && x.IsDeleted == false);
            return UserGroupingUsers;
        }
    }
}
