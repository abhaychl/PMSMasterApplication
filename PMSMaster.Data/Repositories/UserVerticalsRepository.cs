//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IUserVerticalsRepository : IGenericAsyncRepository<UserVerticals>
    {
        List<UserVerticals> GetUserVerticals();
        UserVerticals GetUserVerticalsById(int id);

    }
    public class UserVerticalsRepository : GenericAsyncRepository<UserVerticals>, IUserVerticalsRepository
    {
        public UserVerticalsRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<UserVerticals> GetUserVerticals()
        {
            var listUserVerticals = _dbContext.UserVerticals.Where(x => x.IsDeleted == false).ToList();
            return listUserVerticals;
        }

        public UserVerticals GetUserVerticalsById(int id)
        {
            var UserVerticals = _dbContext.UserVerticals.FirstOrDefault(x => x.UserVerticalId == id);
            return UserVerticals;
        }
    }
}
