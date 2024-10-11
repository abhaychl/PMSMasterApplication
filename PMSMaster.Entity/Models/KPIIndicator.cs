using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMSMaster.Entity.Models
{
    public class KPIIndicator : BaseEntity
    {
        public int KPIIndicatorId { get; set; }
        public string Title { get; set; }        
    }

    public class KPIRule : BaseEntity
    {
        public int KPIRuleId { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }             
        public virtual List<KPIRuleIndicator>? KPIRuleIndicator { get; set; }             
    }

    public class KPIRuleIndicator : BaseEntity
    {
        public int KPIRuleIndicatorId { get; set; }

        private double _quantity;

        public double Quantity
        {
            get => _quantity;
            set
            {
                _quantity = Math.Round(value * 10.0) / 10.0;
            }
        }

        public double Points { get; set; }
        [NotMapped]
        public double? UserAchieved { get; set; }
        [NotMapped]
        public double? UserPoints { get; set; }
        [NotMapped]
        public double? Total { get; set; }
        public int KPIIndicatorId { get; set; }
        public virtual KPIIndicator? KPIIndicator { get; set; }
        public int KPIRuleId { get; set; }
    }

    public class DeskTime : BaseEntity
    {
        public int DeskTimeId { get; set; }
        public int UserID { get; set; }
        public int Duration { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }


}
