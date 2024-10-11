using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class FaqCategory : BaseEntity
    {
        public int FaqCategoryId { get; set; }

        [Required]
        public string Title { get; set; }
    }
}
