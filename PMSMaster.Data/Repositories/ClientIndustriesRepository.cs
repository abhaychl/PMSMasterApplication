//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IClientIndustriesRepository : IGenericAsyncRepository<ClientIndustries>
    {
        List<ClientIndustries> GetClientIndustries();
        ClientIndustries GetClientIndustriesById(int id);

    }
    public class ClientIndustriesRepository : GenericAsyncRepository<ClientIndustries>, IClientIndustriesRepository
    {
        public ClientIndustriesRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<ClientIndustries> GetClientIndustries()
        {
            var listClientIndustries = _dbContext.ClientIndustries.Where(x=>x.IsDeleted == false).ToList();
            return listClientIndustries;
        }

        public ClientIndustries GetClientIndustriesById(int id)
        {
            var clientIndustries = _dbContext.ClientIndustries.FirstOrDefault(x => x.ClientIndustoryId == id);
            return clientIndustries;
        }
    }
}
