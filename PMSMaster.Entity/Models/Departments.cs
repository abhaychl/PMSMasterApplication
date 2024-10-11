using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class Departments : BaseEntity
    {
        public int DepartmentId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
