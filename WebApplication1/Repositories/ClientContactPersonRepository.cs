//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    //public interface IClientContactPersonRepository : IGenericAsyncRepository<ClientContactPerson>
    //{
    //    List<ClientContactPerson> GetClientContactPerson();
    //    ClientContactPerson GetClientContactPersonById(int id);

    //}
    //public class ClientContactPersonRepository : GenericAsyncRepository<ClientContactPerson>, IClientContactPersonRepository
    //{
    //    public ClientContactPersonRepository(ApplicationDBContext dbContext) : base(dbContext)
    //    {
    //    }

    //    public List<ClientContactPerson> GetClientContactPerson()
    //    {
    //        //var listClientContactPerson = _dbContext.ClientContactPerson.Where(x => x.IsDeleted == false).ToList();
    //        //return listClientContactPerson;
    //        return new List<ClientContactPerson>();
    //    }

    //    public ClientContactPerson GetClientContactPersonById(int id)
    //    {
    //        //var ClientContactPersonSourceCategory = _dbContext.ClientContactPerson.FirstOrDefault(x => x.ClientContactPersonId == id);
    //        //return ClientContactPersonSourceCategory;
    //        return new ClientContactPerson();
    //    }
    //}
}
