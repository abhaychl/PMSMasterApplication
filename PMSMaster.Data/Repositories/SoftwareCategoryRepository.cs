//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface ISoftwareCategoryRepository : IGenericAsyncRepository<SoftwareCategory>
    {
        List<SoftwareCategory> GetSoftwareCategory();
        SoftwareCategory GetSoftwareCategoryById(int id);

    }
    public class SoftwareCategoryRepository : GenericAsyncRepository<SoftwareCategory>, ISoftwareCategoryRepository
    {
        public SoftwareCategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<SoftwareCategory> GetSoftwareCategory()
        {
           return  _dbContext.SoftwareCategories.Where(x => x.IsDeleted == false).ToList();
          
        }
        public SoftwareCategory GetSoftwareCategoryById(int id)
        {
           return  _dbContext.SoftwareCategories.FirstOrDefault(x => x.Id == id);
           
        }
    }
}
