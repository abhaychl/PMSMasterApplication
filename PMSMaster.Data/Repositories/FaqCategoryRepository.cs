//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IFaqCategoryRepository : IGenericAsyncRepository<FaqCategory>
    {
        List<FaqCategory> GetFaqCategory();
        FaqCategory GetFaqCategoryById(int id);

    }
    public class FaqCategoryRepository : GenericAsyncRepository<FaqCategory>, IFaqCategoryRepository
    {
        public FaqCategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<FaqCategory> GetFaqCategory()
        {
            var listFaqCategory = _dbContext.FaqCategory.Where(x => x.IsDeleted == false).ToList();
            return listFaqCategory;
        }

        public FaqCategory GetFaqCategoryById(int id)
        {
            var leadSourceCategory = _dbContext.FaqCategory.FirstOrDefault(x => x.FaqCategoryId == id);
            return leadSourceCategory;
        }
    }
}
