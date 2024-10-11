using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class Unit : BaseEntity
    {
        public int Id { get; set; }
        [Required]
        public string Tittle { get; set; }
        
    }
}
