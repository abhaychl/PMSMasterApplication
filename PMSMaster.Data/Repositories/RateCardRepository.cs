//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IRateCardRepository : IGenericAsyncRepository<RateCard>
    {
        List<RateCard> GetRateCard();
        RateCard GetRateCardById(int id);
        RateCard GetRate(int serviceId, int sourceLangId, int targetLangId, int currencyId);
        List<RateCard> GetListRateCardByCategoryId(int id);

    }
    public class RateCardRepository : GenericAsyncRepository<RateCard>, IRateCardRepository
    {
        public RateCardRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<RateCard> GetRateCard()
        {
            var listRateCard = _dbContext.RateCard.Include(x=>x.Services).Include(x => x.Currency).Where(x => x.IsDeleted == false).ToList();
            return listRateCard;
        }

        public RateCard GetRateCardById(int id)
        {
            var leadSourceCategory = _dbContext.RateCard.Include(x => x.Services).Include(x => x.Currency).FirstOrDefault(x => x.RateCardId == id);
            return leadSourceCategory;
        }       
        
        public RateCard GetRate(int serviceId, int sourceLangId, int targetLangId, int currencyId)
        {
            var leadSourceCategory = _dbContext.RateCard.Include(x => x.Services).Include(x => x.Currency).FirstOrDefault(x => x.ServicesServiceId == serviceId && x.SourceLanguageId == sourceLangId && x.TargetLanguageId == targetLangId && x.CurrencyId == currencyId);
            return leadSourceCategory;
        }
        
        public List<RateCard> GetListRateCardByCategoryId(int id)
        {
            var leadSourceCategory = _dbContext.RateCard.Include(x => x.Services).Include(x => x.Currency).Where(x => x.RateCardId == id).ToList();
            return leadSourceCategory;
        }
    }
}
