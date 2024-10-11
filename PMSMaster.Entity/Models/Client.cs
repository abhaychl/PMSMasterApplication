using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PMSMaster.Entity.Models
{
    public class Client : BaseEntity
    {
        public int ClientId { get; set; }        
        public string? CompanyName { get; set; }
        public int ClientIndustoryId { get; set; }
        public int? ClientCategory { get; set; }

        [ForeignKey("ClientIndustoryId")]
        public virtual ClientIndustries? ClientIndustries { get; set; }
        public string ServiceId { get; set; }
        //public virtual Services? Services { get; set; }
        public string Website { get; set; }

        //[Required(ErrorMessage = "PAN is required")]
        //[RegularExpression(@"^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN format")]
        //public string? PAN { get; set; }

        private string _pan = "NA";

        [RegularExpression(@"^NA$|^[A-Z]{5}[0-9]{4}[A-Z]{1}$", ErrorMessage = "Invalid PAN format")]
        public string? PAN
        {
            get { return _pan; }
            set
            {
                // Check if the value is null
                if (value != null)
                {
                    // Perform regular expression validation
                    if (Regex.IsMatch(value, @"^NA$|^[A-Z]{5}[0-9]{4}[A-Z]{1}$"))
                    {
                        _pan = value;
                    }
                    else
                    {
                        // Handle invalid PAN format
                        throw new ArgumentException("Invalid PAN format");
                    }
                }
                else
                {
                    _pan = "NA"; // Set default value
                }
            }
        }

        public string Reference { get; set; }
        public string Remarks { get; set; }
        //[NotMapped]
        //public DateTime? LeadQuoationCreatedOn { get; set; }
        
        public int? AssignToUser { get; set; }
        [NotMapped]
        public string? ClientTransferBy { get; set; }
        [NotMapped]
        public string? ClientAddedBy { get; set; }
        [NotMapped]
        public bool IsQuotationSent { get; set; } = false;
        [NotMapped]
        public string? QuotationSubject { get; set; }
        [NotMapped]
        public DateTime? QuotationCreatedOn { get; set; }
        public virtual List<ClientOffice>? ClientOffices { get; set; } = new List<ClientOffice>();
        public virtual List<ClientRemark>? ClientRemark { get; set; } = new List<ClientRemark>();
    }

    public class ClientOffice : BaseEntity
    {
        public int ClientOfficeId { get; set; }
        public string NameOfLocation { get; set; }
        public string Address { get; set; }

        //[Required(ErrorMessage = "GST is required")]
        //[RegularExpression(@"^[0-9]{2}[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}[0-9A-Za-z]{1}[Z]{1}[0-9A-Za-z]{1}$", ErrorMessage = "Invalid GST format")]
        //public string? GST { get; set; }

        private string _gst = "NA";

        [RegularExpression(@"^NA$|^[0-9]{2}[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}[0-9A-Za-z]{1}[Z]{1}[0-9A-Za-z]{1}$", ErrorMessage = "Invalid GST format")]
        public string? GST
        {
            get { return _gst; }
            set
            {
                // Check if the value is null
                if (value != null)
                {
                    // Perform regular expression validation
                    if (Regex.IsMatch(value, @"^NA$|^[0-9]{2}[A-Za-z]{5}[0-9]{4}[A-Za-z]{1}[0-9A-Za-z]{1}[Z]{1}[0-9A-Za-z]{1}$"))
                    {
                        _gst = value;
                    }
                    else
                    {
                        // Handle invalid GST format
                        throw new ArgumentException("Invalid GST format");
                    }
                }
                else
                {
                    _gst = "NA"; // Set default value
                }
            }
        }

        public string City { get; set; }
        [ForeignKey("StateId")]
        public int StateId { get; set; }
        [ForeignKey("CountryId")]
        public int CountryId { get; set; }
        public string PinCode { get; set; }

       
        public int ClientId { get; set; }
       
        public virtual Countries? Country { get; set; }
        public virtual States? State { get; set; }

        public virtual List<OfficeContactPerson>? ClientContactPerson { get; set; } = new List<OfficeContactPerson>();
    }

    public class OfficeContactPerson : BaseEntity
    {
        public int OfficeContactPersonId { get; set; }
        [ForeignKey("ClientOfficeId")]
        public int ClientOfficeId { get; set; }
       
        //public virtual ClientOffice? ClientOffice { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Designation { get; set; }
        public string PhoneNo { get; set; }
        [NotMapped]
        private string _emailId;

        public string EmailId
        {
            get { return _emailId; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    // Basic email format validation using regular expression
                    if (System.Text.RegularExpressions.Regex.IsMatch(value,
                        @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
                    {
                        _emailId = value;
                    }
                    else
                    {
                        throw new ArgumentException("Invalid email format");
                    }
                }
                else
                {
                    throw new ArgumentNullException("Email cannot be null or empty");
                }
            }
        }
        public string? dummyData { get; set; }
      
    }

    public class ClientRemark : BaseEntity
    {
        public int ClientRemarkId { get; set; }
        [ForeignKey("ClientId")]
        public int ClientId { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public string? Remarks { get; set; }
        public virtual Users? User { get; set; }
    }
}
