using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMSMaster.Entity.Models
{
    public class Users : BaseEntity
    {
        public int UserId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string ClientSecretKey { get; set; }
        [Required]
        public string LoginId { get; set; }
        [Required]
        public string password { get; set; }
        public string? EmployeeCode { get; set; }
        public string? PhoneNo { get; set; }
        public double? TargetAmount { get; set; }
      
        public int? DepartmentId { get; set; }
        public virtual Departments? Department { get; set; }
        public int? UserVerticalId { get; set; }
        public virtual UserVerticals? UserVertical { get; set; }
        public int? OfficeLocationId { get; set; }
        public virtual OfficeLocation? OfficeLocation { get; set; }
        public int? RoleId { get; set; }
        public virtual Role? Role { get; set; }
        //public string Address { get; set; }

        public Users()
        {
            // Set default values in the constructor
            ClientSecretKey = Guid.NewGuid().ToString();
        }
    }
}
