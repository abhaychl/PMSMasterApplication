//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{    
    public interface IStatesRepository : IGenericAsyncRepository<States>
    {
        List<States> GetStates();
        List<States> GetStatesByCountryId(int id);

    }
    public class StatesRepository : GenericAsyncRepository<States>, IStatesRepository
    {
        public StatesRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<States> GetStates()
        {
            var lstStates = _dbContext.States.Where(x => x.IsDeleted == false).ToList();
            return lstStates;
        }

        public List<States> GetStatesByCountryId(int id)
        {
            var States = _dbContext.States.Where(x => x.CountryId == id).ToList();
            return States;
        }
    }

}
