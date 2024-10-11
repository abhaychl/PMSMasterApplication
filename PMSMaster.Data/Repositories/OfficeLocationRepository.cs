//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IOfficeLocationRepository : IGenericAsyncRepository<OfficeLocation>
    {
        List<OfficeLocation> GetOfficeLocation();
        OfficeLocation GetOfficeLocationById(int id);

    }
    public class OfficeLocationRepository : GenericAsyncRepository<OfficeLocation>, IOfficeLocationRepository
    {
        public OfficeLocationRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<OfficeLocation> GetOfficeLocation()
        {
            var listOfficeLocation = _dbContext.OfficeLocation.Where(x => x.IsDeleted == false).ToList();
            return listOfficeLocation;
        }

        public OfficeLocation GetOfficeLocationById(int id)
        {
            var OfficeLocation = _dbContext.OfficeLocation.FirstOrDefault(x => x.OfficeLocationId == id);
            return OfficeLocation;
        }
    }
}
