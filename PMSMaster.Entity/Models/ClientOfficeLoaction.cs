using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace PMSMaster.Entity.Models
{    
    public class ClientOfficeLoaction : BaseEntity
    {
        public int ClientOfficeLoactionId { get; set; }
        public string NameOfLocation { get; set; }
        public string Address { get; set; }        
        public string City { get; set; }
        //[NotMapped]
        //public int StateId { get; set; }
        //[NotMapped]
        //public int CountryId { get; set; }
        public string PinCode { get; set; }

    }
}
