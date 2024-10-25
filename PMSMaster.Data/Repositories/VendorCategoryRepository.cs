//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IVendorCategoryRepository : IGenericAsyncRepository<VendorCategory>
    {
        List<VendorCategory> GetVendorCategory();
        VendorCategory GetVendorCategoryById(int id);

    }
    public class VendorCategoryRepository : GenericAsyncRepository<VendorCategory>, IVendorCategoryRepository
    {
        public VendorCategoryRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

        public List<VendorCategory> GetVendorCategory()
        {
            var vendorTypes = _dbContext.VendorCategorys.AsNoTracking().Where(x => x.IsDeleted == false).ToList();
            return vendorTypes;
        }

        public VendorCategory GetVendorCategoryById(int id)
        {
            var VendorCategorys = _dbContext.VendorCategorys.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return VendorCategorys;
        }
    }
}
