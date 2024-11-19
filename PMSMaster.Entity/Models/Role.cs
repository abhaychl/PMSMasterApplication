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
    public class Role : BaseEntity
    {
        public int RoleId { get; set; }
        public int? Priority { get; set; }

        [Required]
        public string Name { get; set; }

        public int ApplicationId { get; set; }
    }
}
