using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class Services : BaseEntity
    {
        public int ServiceId { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Unit { get; set; }
    }

    public class ServiceType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
