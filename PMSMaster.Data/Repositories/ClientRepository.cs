//using PMSMaster.Data.Interface;
using PMSMaster.Data.DataContext;
using PMSMaster.Entity.Models;
using Microsoft.Data.SqlClient;
using PMSMaster.Data.GenricRepository;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;

namespace PMSMaster.Data.Repositories
{
    public interface IClientRepository : IGenericAsyncRepository<Client>
    {
        List<Client> GetClient(int assignToUser);
        List<Client> GetAllClient();
        Client GetClientById(int id);
        Client GetClientByLeadIdId(int id);
        List<Client> GetClientByMonthAndYear(int selectedMonth, int selectedYear);
        bool ClientExistsForLead(int leadId);
        List<Client> GetClientsWithoutQuotationsCreatedInLastSixMonths(int assignToUser);

    }
    public class ClientRepository : GenericAsyncRepository<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDBContext dbContext) : base(dbContext)
        {
        }

        public List<Client> GetClient(int assignToUser)
        {
            var listClient = _dbContext.Client.Include(x=>x.ClientOffices).Include(x => x.ClientRemark)
                    .ThenInclude(x => x.User).Where(x => x.IsDeleted == false
                && x.AssignToUser == assignToUser).ToList();           

            return listClient;
        }
        public List<Client> GetAllClient()
        {
            var listClient = _dbContext.Client.Include(x=>x.ClientOffices).Include(x => x.ClientRemark).ThenInclude(x => x.User)
                .Where(x => x.IsDeleted == false).ToList();           

            return listClient;
        }

        public List<Client> GetClientByMonthAndYear(int selectedMonth, int selectedYear)
        {
            var firstDayOfMonth = new DateTime(selectedYear, selectedMonth, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var listClient = _dbContext.Client.Include(x => x.ClientOffices)
                .Where(x => x.IsDeleted == false && x.CreatedOn >= firstDayOfMonth && x.CreatedOn <= lastDayOfMonth)
                .ToList();

            return listClient;
        }

        public List<Client> GetClientsWithoutQuotationsCreatedInLastSixMonths(int assignToUser)
        {
            var sixMonthsAgo = DateTime.Now.AddMonths(-6);

            var clientsWithoutQuotations = _dbContext.Client
          .Include(x => x.ClientOffices)
          
          .Where(x => !x.IsDeleted && x.CreatedOn <= sixMonthsAgo)
          //.Where(x => !_dbContext.Quotation.Any(q => q.ClientID == x.ClientId))
          .ToList();

            // Get clients with recent quotations and set IsQuotationSent to true
            var clientsWithRecentQuotations = _dbContext.Client
       .Include(x => x.ClientOffices)
   
       .Where(x => !x.IsDeleted )
       .Select(client => new Client
       {
           ClientId = client.ClientId,
           CompanyName = client.CompanyName,
           CreatedOn = client.CreatedOn,
           IsDeleted = client.IsDeleted,
           IsQuotationSent = true,
           QuotationSubject ="",
           //QuotationSubject = _dbContext.Quotation.Where(q => q.ClientID == client.ClientId && q.CreatedOn <= sixMonthsAgo).Select(q => q.Subject).FirstOrDefault(),
           QuotationCreatedOn=System.DateTime.Now,
           //QuotationCreatedOn = _dbContext.Quotation.Where(q => q.ClientID == client.ClientId && q.CreatedOn <= sixMonthsAgo).Select(q => q.CreatedOn).FirstOrDefault(),
           ClientOffices = client.ClientOffices
          
       })
       .ToList();

            // Combine both lists and ensure unique clients
            var allClients = clientsWithoutQuotations.Concat(clientsWithRecentQuotations)
                                                     .DistinctBy(client => client.ClientId)
                                                     .ToList();

            return allClients;
        }

        public Client GetClientById(int id)
        {
            //var ClientSourceCategory = _dbContext.Client.Include("ClientOffices").Include("OfficeContactPerson").Include("Lead").FirstOrDefault(x => x.ClientId == id);

            var ClientSourceCategory = _dbContext.Client.Include(x => x.ClientOffices).ThenInclude(x => x.Country) // Include Country for ClientOffices
    .Include(x => x.ClientOffices).ThenInclude(x => x.State) // Include State for ClientOffices
    .Include(x => x.ClientOffices).ThenInclude(x => x.ClientContactPerson) // Include State for ClientOffices 
    .FirstOrDefault(x => x.ClientId == id);

            return ClientSourceCategory;
        }
         public Client GetClientByLeadIdId(int id)
        {
            //var ClientSourceCategory = _dbContext.Client.Include("ClientOffices").Include("OfficeContactPerson").Include("Lead").FirstOrDefault(x => x.ClientId == id);

            var ClientSourceCategory = _dbContext.Client
    .Include(x => x.ClientOffices)
        .ThenInclude(x => x.Country) // Include Country for ClientOffices
    .Include(x => x.ClientOffices)
        .ThenInclude(x => x.State) // Include State for ClientOffices
    .Include(x => x.ClientOffices)
        .ThenInclude(x => x.ClientContactPerson) // Include State for ClientOffices 

    .FirstOrDefault();

            return ClientSourceCategory;
        }

        public bool ClientExistsForLead(int leadId)
        {
            return _dbContext.Client.Any();
        }
    }
}
