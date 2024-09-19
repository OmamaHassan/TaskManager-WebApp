using System.ComponentModel.DataAnnotations;

namespace TaskManagerWebApp.Models
{
    public class AddTaskViewModel
    {
        public int Id { get; set; }

        public required string TaskName { get; set; }

        public string? Description { get; set; }

        public DateOnly? DueDate { get; set; }

        public bool IsCompleted { get; set; }

    }
}
