using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSMaster.Entity.Models
{
    public class VendorCategoryDocument : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public bool IsRequired { get; set; }

        public int VendorCategoryId { get; set; }
        public virtual VendorCategory? VendorCategory { get; set; }

        [NotMapped]
        public string? Category { get; set; }

    }
}
