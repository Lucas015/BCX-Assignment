using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class DailyTask
    {
        public DailyTask()
        {
            this.AssignedOn = DateTime.Now;
        }

        [Key]
        public int DailyTaskId { get; set; }
        [ForeignKey("TaskDetail")]
        public int TaskRef { get; set; }
        [Display(Name = "Allocated Time")]
        public int TaskTime { get; set; }
        [ForeignKey("Employee")]
        [Required]
        [Display(Name = "Assign To")]
        public int EmployeeRef { get; set; }
        [Display(Name = "Hourly Rate")]
        public decimal HourlyRate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Assigned On")]
        public DateTime AssignedOn { get; set; }

        public Employee Employee { get; set; }
        public TaskDetail TaskDetail { get; set; }
    }
}
