
namespace PMSMaster.Entity.Models
{
    public class UserGrouping : BaseEntity
    {
        public int UserGroupingId { get; set; }
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }
        public int UserId { get; set; }
        public virtual Users? User { get; set; }
        public virtual List<UserGroupingUsers> UserGroupingUsers { get; set; }
    }

    public class UserGroupingUsers : BaseEntity
    {
        public int UserGroupingUsersId { get; set; }
        public int UserId { get; set; }
        public virtual Users? User { get; set; }
        public int UserGroupingId { get; set; }
    }
}
