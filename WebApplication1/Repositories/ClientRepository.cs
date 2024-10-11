//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IClientRepository : IGenericAsyncRepository<Client>
    {
        List<Client> GetClient();        
        Client GetClientById(int id);

    }
    public class ClientRepository : GenericAsyncRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Client> GetClient()
        {
            var listClient = _dbContext.Client.Where(x => x.IsDeleted == false).ToList();
            return listClient;
        }       

        public Client GetClientById(int id)
        {
            var ClientSourceCategory = _dbContext.Client.FirstOrDefault(x => x.ClientId == id);
            return ClientSourceCategory;
        }
    }
}
