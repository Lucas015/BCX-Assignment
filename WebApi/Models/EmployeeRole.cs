using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class EmployeeRole
    {
        [Key]
        public int RoldeId { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Rate Per Hour")]
        public decimal Rate { get; set; }
    }
}