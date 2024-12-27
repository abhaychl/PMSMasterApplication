//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using PMSMaster.Entity.JsonModels;

namespace PMSMaster.Data.Repositories
{
    public interface ICommonRepository : IGenericAsyncRepository<VendorType>
    {
        Task<dynamic> GetMasterData(MasterDataRequest request);
        Task<List<States>> GetStateByCountryId(int CountryId);
    }
    public class CommonRepository : GenericAsyncRepository<VendorType>, ICommonRepository
    {
        public CommonRepository(ApplicationDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<dynamic> GetMasterData(MasterDataRequest request)
        {
            var response =new  MasterDataResponse();
            
            if(request.IncludeVendorTypes)
                response.VendorTypes = await _dbContext.VendorTypes.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            if (request.IncludeLanguages)
                response.Languages = await _dbContext.Languages.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
           
            if (request.IncludeClientIndustries)
                response.ClientIndustries = await _dbContext.ClientIndustries.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            if (request.IncludeState)
                response.States = await _dbContext.States.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            if (request.IncludeCurrencies)
                response.Currencies = await _dbContext.Currency.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            if (request.IncludeServices)
                response.Services = await _dbContext.Services.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            if (request.IncludeTranslationTools)
                response.TranslationTools = await _dbContext.TranslationTools.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            
            if (request.IncludeCountries)
                response.Countries =await _dbContext.Countries.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            if (request.IncludeVendorCategory)
                response.VendorCategories = await _dbContext.VendorCategorys.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            if (request.IncludeUnits)
                response.Units = await _dbContext.Units.AsNoTracking().Where(x => x.IsDeleted == false).ToListAsync();
            if (request.IncludeServicetype)
                response.ServiceTypes = await _dbContext.ServiceTypes.AsNoTracking().ToListAsync();

            if (request.IncludeUsers)
                response.Users = await _dbContext.Users.AsNoTracking().Select(x=>new { Id=x.UserId,Name=x.Name}).ToListAsync();

            if (request.IncludeWorkType)
                response.WorkTypes = await _dbContext.WorkTypes.AsNoTracking().Select(x => new WorkType{ Id = x.Id, Name = x.Name }).ToListAsync();

            if (request.IncludeWorkNature)
                response.WorkNatures = await _dbContext.WorkNatures.AsNoTracking().Select(x => new WorkNature{ Id = x.Id, Name = x.Name }).ToListAsync();

           
            return response;
        }

        public async Task<List<States>> GetStateByCountryId(int CountryId)
        {
           return  await _dbContext.States.AsNoTracking().Where(x => x.CountryId == CountryId).ToListAsync();            
        }
    }
}
