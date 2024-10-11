//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IKPIRuleIndicatorRepository : IGenericAsyncRepository<KPIRuleIndicator>
    {
        List<KPIRuleIndicator> DeleteKPIRuleIndicatorID(int kpiRuleId);

    }
    public class KPIRuleIndicatorRepository : GenericAsyncRepository<KPIRuleIndicator>, IKPIRuleIndicatorRepository
    {
        public KPIRuleIndicatorRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }
        public List<KPIRuleIndicator> DeleteKPIRuleIndicatorID(int kpiRuleId)
        {
            var ClientContactPersonSourceCategory = _dbContext.KPIRuleIndicator.Where(x=>x.KPIRuleId == kpiRuleId).ToList();
            return ClientContactPersonSourceCategory;
        }
    }
}
