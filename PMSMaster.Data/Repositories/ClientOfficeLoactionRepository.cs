//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IClientOfficeLoactionRepository : IGenericAsyncRepository<ClientOffice>
    {
        List<ClientOffice> GetClientOfficeLoaction();
        ClientOffice GetClientOfficeLoactionById(int id);
        List<ClientOffice> GetClientOfficeLoactionByClientID(int clientId);

    }
    public class ClientOfficeLoactionRepository : GenericAsyncRepository<ClientOffice>, IClientOfficeLoactionRepository
    {
        public ClientOfficeLoactionRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<ClientOffice> GetClientOfficeLoaction()
        {
            var listClientOfficeLoaction = _dbContext.ClientOffices.Where(x=>x.IsDeleted == false).ToList();
            return listClientOfficeLoaction;
        }

        public ClientOffice GetClientOfficeLoactionById(int id)
        {
            var ClientOfficeLoaction = _dbContext.ClientOffices.Include(x=>x.ClientContactPerson).FirstOrDefault(x => x.ClientOfficeId == id);
            return ClientOfficeLoaction;
        }

        public List<ClientOffice> GetClientOfficeLoactionByClientID(int clientId)
        {
            var ClientContactPersonSourceCategory = _dbContext.ClientOffices.Where(x => x.ClientId == clientId).ToList();
            return ClientContactPersonSourceCategory;
        }
    }
}
