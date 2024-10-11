using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class VendorType : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
    }
}
