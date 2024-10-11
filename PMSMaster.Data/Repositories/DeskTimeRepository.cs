//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IDeskTimeRepository : IGenericAsyncRepository<DeskTime>
    {
        List<DeskTime> GetDeskTime();
        DeskTime GetDeskTimeById(int id);
        List<DeskTime> GetListDeskTimeByCategoryId(int id);
        DeskTime GetDeskTimeByMonthAndYear(int month, int year, int assignToUser);

    }
    public class DeskTimeRepository : GenericAsyncRepository<DeskTime>, IDeskTimeRepository
    {
        public DeskTimeRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<DeskTime> GetDeskTime()
        {
            var listDeskTime = _dbContext.DeskTime.Where(x => x.IsDeleted == false).ToList();
            return listDeskTime;
        }

        public DeskTime GetDeskTimeById(int id)
        {
            var leadSourceCategory = _dbContext.DeskTime.FirstOrDefault(x => x.DeskTimeId == id);
            return leadSourceCategory;
        }
         public DeskTime GetDeskTimeByMonthAndYear(int month, int year, int assignToUser)
        {
            var leadSourceCategory = _dbContext.DeskTime.FirstOrDefault(x => x.UserID == assignToUser && x.Month == month && x.Year == year);
            return leadSourceCategory;
        }
        
        public List<DeskTime> GetListDeskTimeByCategoryId(int id)
        {
            var leadSourceCategory = _dbContext.DeskTime.Where(x => x.DeskTimeId == id).ToList();
            return leadSourceCategory;
        }
    }
}
