//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IKPIRuleRepository : IGenericAsyncRepository<KPIRule>
    {
        List<KPIRule> GetKPIRule();
        KPIRule GetKPIRuleById(int id);
        KPIRule GetKPIRuleByRoleId(int id);

    }
    public class KPIRuleRepository : GenericAsyncRepository<KPIRule>, IKPIRuleRepository
    {
        public KPIRuleRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<KPIRule> GetKPIRule()
        {
            var listKPIRule = _dbContext.KPIRule.Include(x=>x.Role).Where(x => x.IsDeleted == false).ToList();
            return listKPIRule;
        }

        public KPIRule GetKPIRuleById(int id)
        {
            var leadSourceCategory = _dbContext.KPIRule.Include(x => x.Role).Include(x=>x.KPIRuleIndicator).ThenInclude(x=>x.KPIIndicator).FirstOrDefault(x => x.KPIRuleId == id);
            return leadSourceCategory;
        } 
        
        public KPIRule GetKPIRuleByRoleId(int id)
        {
            var leadSourceCategory = _dbContext.KPIRule.Include(x => x.Role).Include(x=>x.KPIRuleIndicator).ThenInclude(x=>x.KPIIndicator).FirstOrDefault(x => x.RoleId == id && x.IsDeleted == false);
            return leadSourceCategory;
        }
    }
}
