using System.ComponentModel.DataAnnotations;

namespace AdvWrksDapper.Models
{
    /// <summary>
    /// HR.Department Table of Adventure Works Database 
    /// </summary>
    public class Department
    {
        [Key]
        public int DepartmentID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string GroupName { get; set; }
    }
}
