//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IVendorCategoryDocumentRepository : IGenericAsyncRepository<VendorCategoryDocument>
    {
        List<VendorCategoryDocument> GetCategoryDocument();
        VendorCategoryDocument GetCategoryDocumentById(int id);

    }
    public class VendorCategoryDocumentRepository : GenericAsyncRepository<VendorCategoryDocument>, IVendorCategoryDocumentRepository
    {
        public VendorCategoryDocumentRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

        public List<VendorCategoryDocument> GetCategoryDocument()
        {
                var vendorTypes = (from vd in _dbContext.VendorCategoryDocuments.AsNoTracking()
                                       join vc in _dbContext.VendorCategorys.AsNoTracking() on vd.VendorCategoryId equals vc.Id
                                       where vd.IsDeleted == false
                                       select new VendorCategoryDocument
                                       {
                                           Id = vd.Id,
                                           Name = vd.Name,
                                           CreatedOn = vd.CreatedOn,
                                           ModifiedBy = vd.ModifiedBy,
                                           Status = vd.Status,
                                           Category = vc.Name,
                                           AddedBy = vd.AddedBy,
                                           IsRequired=vd.IsRequired,

                                       }).ToList();

             
            return vendorTypes;
        }

        public VendorCategoryDocument GetCategoryDocumentById(int id)
        {
            var VendorCategoryDocument = _dbContext.VendorCategoryDocuments.AsNoTracking().FirstOrDefault(x => x.Id == id);
            return VendorCategoryDocument;
        }
    }
}
