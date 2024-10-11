//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IUserGroupingRepository : IGenericAsyncRepository<UserGrouping>
    {
        List<UserGrouping> GetUserGrouping();
        UserGrouping GetUserGroupingById(int id);
        UserGrouping GetUserGroupingByUserId(int id);
        UserGrouping GetUserGroupingByUserGroupingIdPasswor(string loginId, string password);

    }
    public class UserGroupingRepository : GenericAsyncRepository<UserGrouping>, IUserGroupingRepository
    {
        public UserGroupingRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<UserGrouping> GetUserGrouping()
        {
            var lstUserGrouping = _dbContext.UserGrouping.Include(x=>x.Role).Include(x => x.User).Include(x => x.UserGroupingUsers).Where(x => x.IsDeleted == false).ToList();
            return lstUserGrouping;
        }

        public UserGrouping GetUserGroupingById(int id)
        {
            var userGrouping = _dbContext.UserGrouping
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.UserGroupingUsers)
                    .ThenInclude(x =>x.User)
                .FirstOrDefault(x => x.UserGroupingId == id);

            //var userGroupingWithUsers = _dbContext.UserGrouping
            //    .Include(x => x.Role)
            //    .Include(x => x.User)
            //    .Include(x => x.UserGroupingUsers)
            //    .Include(x => x.UserGroupingUsers.Select(ugu => ugu.Users))
            //    .FirstOrDefault(x => x.UserGroupingId == id);

            //var usersNotInGrouping = _dbContext.Users
            //    .Where(u => !userGroupingWithUsers.UserGroupingUsers.Any(ugu => ugu.UserId == u.UserId))
            //    .ToList();

            return userGrouping;
        }

        public UserGrouping GetUserGroupingByUserId(int id)
        {
            var userGrouping = _dbContext.UserGrouping
                .Include(x => x.Role)
                .Include(x => x.User)
                .Include(x => x.UserGroupingUsers)
                    .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.UserId == id);

            //var userGroupingWithUsers = _dbContext.UserGrouping
            //    .Include(x => x.Role)
            //    .Include(x => x.User)
            //    .Include(x => x.UserGroupingUsers)
            //    .Include(x => x.UserGroupingUsers.Select(ugu => ugu.Users))
            //    .FirstOrDefault(x => x.UserGroupingId == id);

            //var usersNotInGrouping = _dbContext.Users
            //    .Where(u => !userGroupingWithUsers.UserGroupingUsers.Any(ugu => ugu.UserId == u.UserId))
            //    .ToList();

            return userGrouping;
        }

        public UserGrouping GetUserGroupingByUserGroupingIdPasswor(string loginId, string password)
        {
            var UserGrouping = _dbContext.UserGrouping.Include(x => x.Role).Include(x => x.User).Include(x => x.UserGroupingUsers).FirstOrDefault(x => x.Status && x.IsDeleted == false);
            return UserGrouping;
        }
    }
}
