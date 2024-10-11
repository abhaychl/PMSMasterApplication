//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface ICheckListCategoryRepository : IGenericAsyncRepository<CheckListCategory>
    {
        List<CheckListCategory> GetCheckListCategory();
        CheckListCategory GetCheckListCategoryById(int id);

    }
    public class CheckListCategoryRepository : GenericAsyncRepository<CheckListCategory>, ICheckListCategoryRepository
    {
        public CheckListCategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<CheckListCategory> GetCheckListCategory()
        {
           return  _dbContext.CheckListCategories.Where(x => x.IsDeleted == false).ToList();
          
        }
        public CheckListCategory GetCheckListCategoryById(int id)
        {
           return  _dbContext.CheckListCategories.FirstOrDefault(x => x.Id == id);
           
        }
    }
}
