//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface ITranslationToolsRepository : IGenericAsyncRepository<TranslationTools>
    {
        List<TranslationTools> GetTranslationTools();
        TranslationTools GetTranslationToolsById(int id);

    }
    public class TranslationToolsRepository : GenericAsyncRepository<TranslationTools>, ITranslationToolsRepository
    {
        public TranslationToolsRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<TranslationTools> GetTranslationTools()
        {
           return  _dbContext.TranslationTools.Where(x => x.IsDeleted == false).ToList();
          
        }

        public TranslationTools GetTranslationToolsById(int id)
        {
           return  _dbContext.TranslationTools.FirstOrDefault(x => x.Id == id);
           
        }
    }
}
