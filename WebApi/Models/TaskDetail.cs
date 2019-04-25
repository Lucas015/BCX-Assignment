using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class TaskDetail
    {
        [Key]
        public int TaskId { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        public string TaskName { get; set; }

        [Display(Name = "Task Description")]
        public string TaskDescription { get; set; }

        [Required]
        [Range(typeof(int), "1", "12", ErrorMessage = "Task Duration should be between 1 and 12 hours")]
        [Display(Name = "Task Duration")]
        public int TaskDuration { get; set; }
    }
}