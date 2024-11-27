using NuGet.Protocol;
using PMSMaster.Entity.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSMaster.Entity.JsonModels
{
    public class MasterDataRequest
    {
        public bool IncludeVendorTypes { get; set; }
        public bool IncludeLanguages { get; set; }
        public bool IncludeClientIndustries { get; set; }
        public bool IncludeCurrencies { get; set; }
        public bool IncludeServices { get; set; }
        public bool IncludeTranslationTools { get; set; }
        public bool IncludeCountries { get; set; }
        public bool IncludeState { get; set; }
        public bool IncludeVendorCategory { get; set; }
        public bool IncludeUnits { get; set; }
        public bool IncludeServicetype { get; set; }
        public bool IncludeUsers { get; set; }
    }
    public class MasterDataResponse
    {
        public List<VendorType> VendorTypes { get; set; }
        public List<Language> Languages { get; set; }
        public List<ClientIndustries> ClientIndustries { get; set; }
        public List<Currency> Currencies { get; set; }
        public List<Services> Services { get; set; }
        public List<TranslationTools> TranslationTools { get; set; }
        public List<Countries> Countries { get; set; }
        public List<States> States { get; set; }
        public List<VendorCategory> VendorCategories { get; set; }
        public List<Unit> Units { get; set; }
        public List<PMSMaster.Entity.Models.ServiceType> ServiceTypes { get; set; }

        public dynamic Users { get; set; }
    }
}
