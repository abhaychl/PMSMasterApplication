using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class Currency : BaseEntity
    {
        public int CurrencyId { get; set; }

        [Required]
        public string Name { get; set; }
        public double? ConvertionRate { get; set; }
    }
}
