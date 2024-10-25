//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IRequisitionRepository : IGenericAsyncRepository<VendorType>
    {
        Task<dynamic> GetRequisitionFormData();
    }
    public class RequisitionRepository : GenericAsyncRepository<VendorType>, IRequisitionRepository
    {
        public RequisitionRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<dynamic> GetRequisitionFormData()
        {
            var vendorTypes =await _dbContext.VendorTypes.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            var languages =await  _dbContext.Languages.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            var clientIndustries =await  _dbContext.ClientIndustries.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            var currencies =await  _dbContext.Currency.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();

            var formData = new
            {
                vendorTypes = vendorTypes,
                languages = languages,
                clientIndustries = clientIndustries,
                currencies = currencies
            };
            return formData;
        }
    }
}
