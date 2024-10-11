//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface ICountriesRepository : IGenericAsyncRepository<Countries>
    {
        List<Countries> GetCountries();
        Countries GetCountriesById(int id);

    }
    public class CountriesRepository : GenericAsyncRepository<Countries>, ICountriesRepository
    {
        public CountriesRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Countries> GetCountries()
        {
            var lstCountries = _dbContext.Countries.Where(x => x.IsDeleted == false).ToList();
            return lstCountries;
        }

        public Countries GetCountriesById(int id)
        {
            var Countries = _dbContext.Countries.FirstOrDefault(x => x.CountryId == id);
            return Countries;
        }
    }

    public interface ICitiesRepository : IGenericAsyncRepository<Cities>
    {
        List<Cities> GetCities();
        Cities GetCitiesById(int id);

    }
    public class CitiesRepository : GenericAsyncRepository<Cities>, ICitiesRepository
    {
        public CitiesRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Cities> GetCities()
        {
            var lstCities = _dbContext.Cities.Where(x => x.IsDeleted == false).ToList();
            return lstCities;
        }

        public Cities GetCitiesById(int id)
        {
            var Cities = _dbContext.Cities.FirstOrDefault(x => x.CountryId == id);
            return Cities;
        }
    }

}
