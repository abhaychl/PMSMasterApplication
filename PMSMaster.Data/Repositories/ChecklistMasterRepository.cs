//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface ICheckListMasterRepository : IGenericAsyncRepository<CheckListMaster>
    {
        List<CheckListMaster> GetCheckListMaster();
        CheckListMaster GetCheckListMasterById(int id);

    }
    public class CheckListMasterRepository : GenericAsyncRepository<CheckListMaster>, ICheckListMasterRepository
    {
        public CheckListMasterRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<CheckListMaster> GetCheckListMaster()
        {
            var checklistmaster = (from cm in _dbContext.CheckListMasters.AsNoTracking()
                                   join ck in _dbContext.CheckListCategories.AsNoTracking() on cm.CheckListCategoryId equals ck.Id
                                   where cm.IsDeleted == false
                                   select new CheckListMaster
                                   {
                                       Id = cm.Id,
                                       Name = cm.Name,
                                       CreatedOn = cm.CreatedOn,
                                       ModifiedBy = cm.ModifiedBy,
                                       Status=cm.Status,
                                       Category = ck.Name,
                                       AddedBy=cm.AddedBy
                                       

                                   }).ToList();

            return checklistmaster;
        }
        public CheckListMaster GetCheckListMasterById(int id)
        {
            return _dbContext.CheckListMasters.FirstOrDefault(x => x.Id == id);

        }
    }
}
