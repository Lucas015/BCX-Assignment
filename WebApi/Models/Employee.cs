using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Surnme")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get { return string.Format("{0} {1}", LastName, FirstName); } }


        [ForeignKey("EmployeeRole")]
        public int EmployeeRoleRef { get; set; }

        public EmployeeRole EmployeeRole { get; set; }
}
}