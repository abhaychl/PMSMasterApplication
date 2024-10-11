using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSMaster.Entity.Models
{
    public class Email : BaseEntity
    {
        public int EmailId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }        
    }
}
