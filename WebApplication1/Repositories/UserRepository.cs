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
        Users GetUserById(int id);
        Users GetUserByuserIdPasswor(string loginId, string password);
        Users GetUserByClientSecret(string clientID);

    }
    public class UserRepository : GenericAsyncRepository<Users>, IUserRepository
    {
        public UserRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public Users GetUserById(int id)
        {
            var user = _dbContext.Users.Include("Department").Include("Role").Include("UserVertical").Include("OfficeLocation").FirstOrDefault(x=>x.UserId == id);
            return user;
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

        public Users GetUserByClientSecret(string clientID)
        {
            var user = _dbContext.Users.Include("Department").Include("Role").Include("UserVertical").Include("OfficeLocation").FirstOrDefault(x => x.ClientSecretKey == clientID);
            return user;
        }
    }
}
