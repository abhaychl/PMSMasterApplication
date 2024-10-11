//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IKPIIndicatorRepository : IGenericAsyncRepository<KPIIndicator>
    {
        List<KPIIndicator> GetKPIIndicator();
        KPIIndicator GetKPIIndicatorById(int id);

    }
    public class KPIIndicatorRepository : GenericAsyncRepository<KPIIndicator>, IKPIIndicatorRepository
    {
        public KPIIndicatorRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<KPIIndicator> GetKPIIndicator()
        {
            var listKPIIndicator = _dbContext.KPIIndicator.Where(x => x.IsDeleted == false).ToList();
            return listKPIIndicator;
        }

        public KPIIndicator GetKPIIndicatorById(int id)
        {
            var leadSourceCategory = _dbContext.KPIIndicator.FirstOrDefault(x => x.KPIIndicatorId == id);
            return leadSourceCategory;
        }
    }
}
