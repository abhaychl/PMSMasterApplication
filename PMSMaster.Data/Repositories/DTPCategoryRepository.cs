//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IDTPCategoryRepository : IGenericAsyncRepository<DTPCategory>
    {
        List<DTPCategory> GetDTPCategory();
        DTPCategory GetDTPCategoryById(int id);

    }
    public class DTPCategoryRepository : GenericAsyncRepository<DTPCategory>, IDTPCategoryRepository
    {
        public DTPCategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<DTPCategory> GetDTPCategory()
        {
           return  _dbContext.DTPCategories.Where(x => x.IsDeleted == false).ToList();
          
        }
        public DTPCategory GetDTPCategoryById(int id)
        {
           return  _dbContext.DTPCategories.FirstOrDefault(x => x.Id == id);           
        }
    }
}
