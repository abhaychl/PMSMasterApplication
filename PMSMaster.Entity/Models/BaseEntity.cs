
namespace PMSMaster.Entity.Models
{
    public abstract class BaseEntity
    {
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }
        private DateTime _CreatedOn;
        public DateTime CreatedOn
        {
            get
            {
                if (_CreatedOn == DateTime.MinValue)
                    return DateTime.Now;
                return _CreatedOn;
            }
            set
            {
                _CreatedOn = value;
            }
        }
        private DateTime _ModifiedOn;
        public DateTime ModifiedOn
        {
            get
            {
                if (_ModifiedOn == DateTime.MinValue)
                    return DateTime.Now;
                return _ModifiedOn;
            }
            set
            {
                _ModifiedOn = value;
            }
        }
        public int AddedBy { get; set; }
        public int ModifiedBy { get; set; }
    }
}
