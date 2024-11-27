using PMSMaster.Data.Repositories;
using PMSMaster.Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.DataContext
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
            // Configuration.LazyLoadingEnabled = false;
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ClientIndustries> ClientIndustries { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Services> Services { get; set; }
       
        public DbSet<Departments> Departments { get; set; }
        public DbSet<OfficeLocation> OfficeLocation { get; set; }
        public DbSet<UserVerticals> UserVerticals { get; set; }
        public DbSet<Designation> Designation { get; set; }
        public DbSet<FinancialYear> FinancialYear { get; set; }
        public DbSet<FaqCategory> FaqCategory { get; set; }
        public DbSet<KPIIndicator> KPIIndicator { get; set; }
        public DbSet<KPIRule> KPIRule { get; set; }
        public DbSet<KPIRuleIndicator> KPIRuleIndicator { get; set; }
        public DbSet<UserGrouping> UserGrouping { get; set; }
        public DbSet<UserGroupingUsers> UserGroupingUsers { get; set; }
        public DbSet<Faq> Faq { get; set; }
        public DbSet<DeskTime> DeskTime { get; set; }
        public DbSet<Email> Email { get; set; }
     
        public DbSet<Client> Client { get; set; }
        public DbSet<ClientOffice> ClientOffices { get; set; }        
        public DbSet<ClientRemark> ClientRemark { get; set; }        
        public DbSet<OfficeContactPerson> OfficeContactPerson { get; set; }
        //public DbSet<ClientOffice> ClientOffices { get; set; }
        //public DbSet<OfficeContactPerson> OfficeContactPerson { get; set; }
        public DbSet<Countries> Countries { get; set; }
        public DbSet<States> States { get; set; }
        public DbSet<Cities> Cities { get; set; }
        
        public DbSet<RateCard> RateCard { get; set; }
        
        public DbSet<Unit> Units { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<TranslationTools> TranslationTools { get; set; }
        public DbSet<DTPCategory> DTPCategories { get; set; }
        public DbSet<SoftwareCategory> SoftwareCategories { get; set; }
        public DbSet<CheckListCategory> CheckListCategories { get; set; }
        public DbSet<CheckListMaster> CheckListMasters { get; set; }
        public DbSet<VendorType> VendorTypes { get; set; }
        public DbSet<VendorCategory> VendorCategorys { get; set; }
        public DbSet<VendorCategoryDocument> VendorCategoryDocuments { get; set; }

        public DbSet<Applications> Applications { get; set; }
        public DbSet<ServiceType> ServiceTypes { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>().HasKey(x => x.UserId);
            modelBuilder.Entity<ClientIndustries>().HasKey(ci => ci.ClientIndustoryId);
            modelBuilder.Entity<Services>().HasKey(ci => ci.ServiceId);
            modelBuilder.Entity<Departments>().HasKey(ci => ci.DepartmentId);
            modelBuilder.Entity<OfficeLocation>().HasKey(ci => ci.OfficeLocationId);
            modelBuilder.Entity<UserVerticals>().HasKey(ci => ci.UserVerticalId);

            modelBuilder.Entity<Cities>().HasKey(ci => ci.CitiesId);
            modelBuilder.Entity<States>().HasKey(ci => ci.StateId);
            modelBuilder.Entity<Countries>().HasKey(ci => ci.CountryId);

            //modelBuilder.Entity<ClientOfficeLoactions>().HasKey(ci => ci.ClientOfficeLoactionId);
            //modelBuilder.Entity<ClientOffices>().HasKey(ci => ci.ClientOfficeLoactionId);
            //modelBuilder.Entity<ClientOffices>().HasOptional(c => c.Lead)
            //.WithMany()
            //.HasForeignKey(c => c.LeadId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
