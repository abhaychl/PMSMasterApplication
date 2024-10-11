using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class CheckListCategory : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
      
    }
}
