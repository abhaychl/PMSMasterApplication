//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IDesignationRepository : IGenericAsyncRepository<Designation>
    {
        List<Designation> GetDesignation();
        Designation GetDesignationById(int id);

    }
    public class DesignationRepository : GenericAsyncRepository<Designation>, IDesignationRepository
    {
        public DesignationRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Designation> GetDesignation()
        {
            var listDesignation = _dbContext.Designation.Where(x => x.IsDeleted == false).ToList();
            return listDesignation;
        }

        public Designation GetDesignationById(int id)
        {
            var leadSourceCategory = _dbContext.Designation.FirstOrDefault(x => x.DesignationId == id);
            return leadSourceCategory;
        }
    }
}
