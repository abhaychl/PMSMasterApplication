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
        //List<ClientOffice> GetClientOfficeLoaction();
        //ClientOffice GetClientOfficeLoactionById(int id);

    }
    public class ClientOfficeLoactionRepository : GenericAsyncRepository<ClientOffice>, IClientOfficeLoactionRepository
    {
        public ClientOfficeLoactionRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        //public List<ClientOffice> GetClientOfficeLoaction()
        //{
        //    var listClientOfficeLoaction = _dbContext.ClientOffice.Where(x=>x.IsDeleted == false).ToList();
        //    return listClientOfficeLoaction;
        //}

        //public ClientOffice GetClientOfficeLoactionById(int id)
        //{
        //    var ClientOfficeLoaction = _dbContext.ClientOffice.FirstOrDefault(x => x.ClientOfficeLoactionId == id);
        //    return ClientOfficeLoaction;
        //}
    }
}
