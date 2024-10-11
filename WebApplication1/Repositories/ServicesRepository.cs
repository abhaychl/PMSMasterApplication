//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IServicesRepository : IGenericAsyncRepository<Services>
    {
        List<Services> GetServices();
        Services GetServicesById(int id);

    }
    public class ServicesRepository : GenericAsyncRepository<Services>, IServicesRepository
    {
        public ServicesRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Services> GetServices()
        {
            var listServices = _dbContext.Services.Where(x => x.IsDeleted == false).ToList();
            return listServices;
        }

        public Services GetServicesById(int id)
        {
            var clientIndustries = _dbContext.Services.FirstOrDefault(x => x.ServiceId == id);
            return clientIndustries;
        }
    }
}
