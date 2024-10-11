using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSMaster.Entity.Models
{
    public class Faq : BaseEntity
    {
        public int FaqId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }

        [ForeignKey("FaqCategoryId")]
        public int FaqCategoryId { get; set; }

        public virtual FaqCategory? FaqCategory { get; set; }
    }
}
