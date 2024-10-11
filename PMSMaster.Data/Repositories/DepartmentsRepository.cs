//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IDepartmentsRepository : IGenericAsyncRepository<Departments>
    {
        List<Departments> GetDepartments();
        Departments GetDepartmentsById(int id);

    }
    public class DepartmentsRepository : GenericAsyncRepository<Departments>, IDepartmentsRepository
    {
        public DepartmentsRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Departments> GetDepartments()
        {
            var listDepartments = _dbContext.Departments.Where(x => x.IsDeleted == false).ToList();
            return listDepartments;
        }

        public Departments GetDepartmentsById(int id)
        {
            var leadSourceCategory = _dbContext.Departments.FirstOrDefault(x => x.DepartmentId == id);
            return leadSourceCategory;
        }
    }
}
