//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface ICurrencyRepository : IGenericAsyncRepository<Currency>
    {
        List<Currency> GetCurrency();
        Currency GetCurrencyById(int id);

    }
    public class CurrencyRepository : GenericAsyncRepository<Currency>, ICurrencyRepository
    {
        public CurrencyRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Currency> GetCurrency()
        {
            var listCurrency = _dbContext.Currency.Where(x => x.IsDeleted == false).ToList();
            return listCurrency;
        }

        public Currency GetCurrencyById(int id)
        {
            var clientIndustries = _dbContext.Currency.FirstOrDefault(x => x.CurrencyId == id);
            return clientIndustries;
        }
    }
}
