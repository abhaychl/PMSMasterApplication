//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IVendorTypeRepository : IGenericAsyncRepository<VendorType>
    {
        List<VendorType> GetVendorTypes();
        VendorType GetVendorTypeById(int id);

    }
    public class VendorTypeRepository : GenericAsyncRepository<VendorType>, IVendorTypeRepository
    {
        public VendorTypeRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

        public List<VendorType> GetVendorTypes()
        {
            var vendorTypes = _dbContext.VendorTypes.AsNoTracking().Where(x => x.IsDeleted == false).ToList();
            return vendorTypes;
        }

        public VendorType GetVendorTypeById(int id)
        {
            var vendorTypes = _dbContext.VendorTypes.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return vendorTypes;
        }
    }
}
