//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface ILanguageRepository : IGenericAsyncRepository<Language>
    {
        List<Language> GetLanguage();
        Language GetLanguageById(int id);

    }
    public class LanguageRepository : GenericAsyncRepository<Language>, ILanguageRepository
    {
        public LanguageRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Language> GetLanguage()
        {
           return _dbContext.Languages.Where(x => x.IsDeleted == false).ToList();
           
        }

        public Language GetLanguageById(int id)
        {
            return _dbContext.Languages.FirstOrDefault(x => x.Id == id);
          
        }
    }
}
