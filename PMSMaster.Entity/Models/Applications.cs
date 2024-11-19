using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PMSMaster.Entity.Models
{
    public class Applications 
    {
        public int Id { get; set; }        
        public string Name { get; set; }
       
    }

  
}
