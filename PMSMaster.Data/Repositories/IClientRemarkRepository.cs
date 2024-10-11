//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IClientRemarkRepository : IGenericAsyncRepository<ClientRemark>
    {
        List<ClientRemark> GetClientRemark();
        ClientRemark GetClientRemarkById(int id);
        List<ClientRemark> GetClientRemarkByAppointmentId(int appointmentId);

    }
    public class ClientRemarkRepository : GenericAsyncRepository<ClientRemark>, IClientRemarkRepository
    {
        public ClientRemarkRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<ClientRemark> GetClientRemark()
        {
            var listClientRemark = _dbContext.ClientRemark.Where(x => x.IsDeleted == false).ToList();
            return listClientRemark;
        }

        public ClientRemark GetClientRemarkById(int id)
        {
            var ClientRemarkSourceCategory = _dbContext.ClientRemark.FirstOrDefault(x => x.ClientRemarkId == id);
            return ClientRemarkSourceCategory;
        } 
        
        public List<ClientRemark> GetClientRemarkByAppointmentId(int ClientId)
        {
            var ClientRemarkSourceCategory = _dbContext.ClientRemark.Include(x=>x.User).Where(x => x.ClientId == ClientId).ToList();
            return ClientRemarkSourceCategory;
        }
    }
}
