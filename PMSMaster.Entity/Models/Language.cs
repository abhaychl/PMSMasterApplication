using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class Language : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int LanguageType { get; set; }
    }
}
