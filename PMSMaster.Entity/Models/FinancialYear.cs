using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class FinancialYear : BaseEntity
    {
        public int FinancialYearId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
