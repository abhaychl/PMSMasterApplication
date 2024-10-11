//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IFaqRepository : IGenericAsyncRepository<Faq>
    {
        List<Faq> GetFaq();
        Faq GetFaqById(int id);
        List<Faq> GetListFaqByCategoryId(int id);

    }
    public class FaqRepository : GenericAsyncRepository<Faq>, IFaqRepository
    {
        public FaqRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Faq> GetFaq()
        {
            var listFaq = _dbContext.Faq.Include(x=>x.FaqCategory).Where(x => x.IsDeleted == false).ToList();
            return listFaq;
        }

        public Faq GetFaqById(int id)
        {
            var leadSourceCategory = _dbContext.Faq.Include(x => x.FaqCategory).FirstOrDefault(x => x.FaqId == id);
            return leadSourceCategory;
        }
        
        public List<Faq> GetListFaqByCategoryId(int id)
        {
            var leadSourceCategory = _dbContext.Faq.Include(x => x.FaqCategory).Where(x => x.FaqCategoryId == id).ToList();
            return leadSourceCategory;
        }
    }
}
