//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IUserRepository : IGenericAsyncRepository<Users>
    {
        List<Users> GetUsers();
        Task<List<Users>> GetLMSUsers();
        Users GetUserById(int id);
        List<Users> GetUsersByRoleID(int id);
        List<Users> GetUsersWithoutGroup(int id);
        Users GetUserByuserIdPasswor(string loginId, string password);
        Users GetUserByClientSecret(string clientID);
        Task<List<Users>> GetQCUsers();

    }
    public class UserRepository : GenericAsyncRepository<Users>, IUserRepository
    {
        public UserRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public Users GetUserById(int id)
        {
            var user = _dbContext.Users.Include("Department").Include("Role").Include("UserVertical").Include("OfficeLocation").FirstOrDefault(x => x.UserId == id);
            return user;
        }
        public List<Users> GetUsersByRoleID(int id)
        {
            var users = _dbContext.Users
                .Include(x => x.Department)
                .Include(x => x.Role)
                .Include(x => x.UserVertical)
                .Include(x => x.OfficeLocation)
                .Where(x => x.RoleId == id &&
                            x.IsDeleted == false)
                .ToList();

            return users;
        }

        public List<Users> GetUsersWithoutGroup(int id)
        {
            if (id < 1)
            {
                var res = new List<Users>();
                res.Add(new Users());

                return res;
            }

            var getPriorityId = GetUserById(id).Role.Priority;

            var usersWithoutGroup = _dbContext.Users
                .Include(x => x.Role)
                .Where(u => !_dbContext.UserGroupingUsers
                    .Any(ugu => ugu.UserId == u.UserId && !ugu.IsDeleted) &&
                    u.Role.Priority > getPriorityId &&
                    u.IsDeleted == false)
                .ToList();

            return usersWithoutGroup;
        }

        public Users GetUserByuserIdPasswor(string loginId, string password)
        {
            var user = _dbContext.Users.Include("Department").Include("Role").Include("UserVertical").Include("OfficeLocation").FirstOrDefault(x => x.LoginId == loginId && x.password == password && x.Status);
            return user;
        }

        public List<Users> GetUsers()
        {
            var lstUser = _dbContext.Users.Include("Department").Include("Role").Include("UserVertical").Include("OfficeLocation").Where(x => x.IsDeleted == false).ToList();
            return lstUser;
        }
        public async Task<List<Users>> GetLMSUsers()
        {
            var lstUser = await (from u in _dbContext.Users.AsNoTracking()
                                 join rol in _dbContext.Role.AsNoTracking() on u.RoleId equals rol.RoleId
                                 where rol.ApplicationId == 2
                                 select u).ToListAsync();
            return lstUser;
        }

        public Users GetUserByClientSecret(string clientID)
        {
            var user = _dbContext.Users.Include("Department").Include("Role").Include("UserVertical").Include("OfficeLocation").FirstOrDefault(x => x.ClientSecretKey == clientID);
            return user;
        }

        public async Task<List<Users>> GetQCUsers()
        {
            var lstUser = await (from u in _dbContext.Users.AsNoTracking()
                                 join rol in _dbContext.Role.AsNoTracking() on u.RoleId equals rol.RoleId
                                 where (rol.RoleId == 5 || rol.RoleId == 11)
                                 select u).OrderBy(c=>c.Name).ToListAsync();
            return lstUser;
        }
    }
}
