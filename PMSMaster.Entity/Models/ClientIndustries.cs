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
    public class ClientIndustries : BaseEntity
    {
        public int ClientIndustoryId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
