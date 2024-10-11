//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IFinancialYearRepository : IGenericAsyncRepository<FinancialYear>
    {
        List<FinancialYear> GetFinancialYear();
        FinancialYear GetFinancialYearById(int id);

    }
    public class FinancialYearRepository : GenericAsyncRepository<FinancialYear>, IFinancialYearRepository
    {
        public FinancialYearRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<FinancialYear> GetFinancialYear()
        {
            var listFinancialYear = _dbContext.FinancialYear.Where(x => x.IsDeleted == false).ToList();
            return listFinancialYear;
        }

        public FinancialYear GetFinancialYearById(int id)
        {
            var leadSourceCategory = _dbContext.FinancialYear.FirstOrDefault(x => x.FinancialYearId == id);
            return leadSourceCategory;
        }
    }
}
