﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PMSMaster.Entity.Models
{
    public class OfficeLocation : BaseEntity
    {
        public int OfficeLocationId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
