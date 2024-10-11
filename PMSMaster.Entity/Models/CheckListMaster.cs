using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSMaster.Entity.Models
{
    public class CheckListMaster : BaseEntity
    {
        public int Id { get; set; }

        public int CheckListCategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        [NotMapped]
        public string? Category { get; set; }

    }
}
