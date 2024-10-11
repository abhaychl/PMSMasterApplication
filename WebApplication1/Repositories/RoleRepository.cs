//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IRoleRepository : IGenericAsyncRepository<Role>
    {
        List<Role> GetRole();
        Role GetRoleById(int id);
        Role GetRoleByRoleIdPasswor(string loginId, string password);

    }
    public class RoleRepository : GenericAsyncRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Role> GetRole()
        {
            var lstRole = _dbContext.Role.Where(x => x.IsDeleted == false).ToList();
            return lstRole;
        }

        public Role GetRoleById(int id)
        {
            var Role = _dbContext.Role.FirstOrDefault(x => x.RoleId == id);
            return Role;
        }

        public Role GetRoleByRoleIdPasswor(string loginId, string password)
        {
            var Role = _dbContext.Role.FirstOrDefault(x => x.Status && x.IsDeleted == false);
            return Role;
        }
    }
}
