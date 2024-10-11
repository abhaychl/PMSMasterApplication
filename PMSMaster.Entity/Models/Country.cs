using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class Countries : BaseEntity
    {
        public int CountryId { get; set; }
        [Required(ErrorMessage = "Country is required")]
        public string Name { get; set; }
    }
    public class States : BaseEntity
    {
        public int StateId { get; set; }
        public int CountryId { get; set; }
        public virtual Countries Country { get; set; }
        public string Name { get; set; }
        public string? ShortName { get; set; }
        public int? StateCode { get; set; }
    }

    public class Cities : BaseEntity
    {
        public int CitiesId { get; set; }
        public int CountryId { get; set; }
        public virtual Countries Country { get; set; }
        public int StateId { get; set; }
        public virtual States State { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
