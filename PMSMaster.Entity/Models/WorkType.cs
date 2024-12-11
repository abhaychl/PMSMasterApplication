using System.ComponentModel.DataAnnotations;

namespace PMSMaster.Entity.Models
{
    public class WorkType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

    public class WorkNature
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
