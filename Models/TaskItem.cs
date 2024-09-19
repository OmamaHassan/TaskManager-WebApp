using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Common;

namespace TaskManagerWebApp.Models
{
    public class TaskItem
    {

        public int Id { get; set; }

        public required string TaskName { get; set ; }

        public string? Description { get; set; } 

        public DateOnly? DueDate { get; set; }

        public bool IsCompleted { get; set; }

    }
}
