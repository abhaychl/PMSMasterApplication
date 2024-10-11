using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class Designation : BaseEntity
    {
        public int DesignationId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
