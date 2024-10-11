//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IClientContactPersonRepository : IGenericAsyncRepository<OfficeContactPerson>
    {
        List<OfficeContactPerson> GetClientContactPerson();
        OfficeContactPerson GetClientContactPersonById(int id);
        List<OfficeContactPerson> GetClientContactPersonByClientID(int clientId);
        List<OfficeContactPerson> DeleteClientContactPersonByOfficeLocationID(int clientId);
        List<ClientOffice> GetClientOfficeLocationByClientID(int clientId);
        List<OfficeContactPerson> GetClientContactPersonByOfficeID(int officeId);

    }
    public class ClientContactPersonRepository : GenericAsyncRepository<OfficeContactPerson>, IClientContactPersonRepository
    {
        public ClientContactPersonRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<OfficeContactPerson> GetClientContactPerson()
        {
            var listClientContactPerson = _dbContext.OfficeContactPerson.Where(x => x.IsDeleted == false).ToList();
            return listClientContactPerson;
            //return new List<OfficeContactPerson>();
        }

        public OfficeContactPerson GetClientContactPersonById(int id)
        {
            var ClientContactPersonSourceCategory = _dbContext.OfficeContactPerson.FirstOrDefault(x => x.OfficeContactPersonId == id);
            return ClientContactPersonSourceCategory;
            //return null;
        }
        public List<OfficeContactPerson> GetClientContactPersonByClientID(int clientId)
        {
            var ClientContactPersonSourceCategory = _dbContext.OfficeContactPerson.Where(x => x.ClientOfficeId == clientId).ToList();
            //var ClientContactPersonSourceCategory = _dbContext.OfficeContactPerson.ToList();
            return ClientContactPersonSourceCategory;
            //return new List<OfficeContactPerson>();
        } 
        
        public List<ClientOffice> GetClientOfficeLocationByClientID(int clientId)
        {
            var ClientContactPersonSourceCategory = _dbContext.ClientOffices.Where(x => x.ClientId== clientId).ToList();
            //var ClientContactPersonSourceCategory = _dbContext.OfficeContactPerson.ToList();
            return ClientContactPersonSourceCategory;
            //return new List<OfficeContactPerson>();
        } 
        
        public List<OfficeContactPerson> GetClientContactPersonByOfficeID(int officeId)
        {
            var ClientContactPersonSourceCategory = _dbContext.OfficeContactPerson.Where(x => x.ClientOfficeId == officeId).ToList();
            //var ClientContactPersonSourceCategory = _dbContext.OfficeContactPerson.ToList();
            return ClientContactPersonSourceCategory;
            //return new List<OfficeContactPerson>();
        }
        
        public List<OfficeContactPerson> DeleteClientContactPersonByOfficeLocationID(int officeLocationId)
        {
            var ClientContactPersonSourceCategory = _dbContext.OfficeContactPerson.Where(x => x.ClientOfficeId == officeLocationId).ToList();
            return ClientContactPersonSourceCategory;
            //return new List<OfficeContactPerson>();
        }
    }
}
