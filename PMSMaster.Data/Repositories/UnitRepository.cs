//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IUnitRepository : IGenericAsyncRepository<Unit>
    {
         Task<List<Unit>> GetUnits();
        Task<Unit> GetUnitById(int id);

    }
    public class UnitRepository : GenericAsyncRepository<Unit>, IUnitRepository
    {
        public UnitRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Unit>> GetUnits()
        {
            var listServices = await _dbContext.Units.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            return listServices;
        }

        public async Task<Unit> GetUnitById(int id)
        {
            var clientIndustries = await _dbContext.Units.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return clientIndustries;
        }
    }
}
