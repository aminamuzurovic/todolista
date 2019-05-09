using System.ComponentModel.DataAnnotations;

namespace ToDoApp
{
    public class TaskModel 
    {
        public int TaskID { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "TaskName")]
        public string TaskName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "TaskDescription")]
        public string TaskDescription { get; set; }
    }

 
}