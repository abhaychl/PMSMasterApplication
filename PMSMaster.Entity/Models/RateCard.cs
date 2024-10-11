using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSMaster.Entity.Models
{
    public class RateCard : BaseEntity
    {
        public int RateCardId { get; set; }
        public int ServicesServiceId { get; set; }
        public int SourceLanguageId { get; set; }
        public int TargetLanguageId { get; set; }
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }
        public double Price { get; set; }
        public virtual Services? Services { get; set; }
        public virtual Currency? Currency { get; set; }
    }
}
