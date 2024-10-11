//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IEmailRepository : IGenericAsyncRepository<Email>
    {
        List<Email> GetEmail();
        Email GetEmailById(int id);
        List<Email> GetListEmailByCategoryId(int id);

    }
    public class EmailRepository : GenericAsyncRepository<Email>, IEmailRepository
    {
        public EmailRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Email> GetEmail()
        {
            var listEmail = _dbContext.Email.Where(x => x.IsDeleted == false).ToList();
            return listEmail;
        }

        public Email GetEmailById(int id)
        {
            var leadSourceCategory = _dbContext.Email.FirstOrDefault(x => x.EmailId == id);
            return leadSourceCategory;
        }
        
        public List<Email> GetListEmailByCategoryId(int id)
        {
            var leadSourceCategory = _dbContext.Email.Where(x => x.EmailId == id).ToList();
            return leadSourceCategory;
        }
    }
}
