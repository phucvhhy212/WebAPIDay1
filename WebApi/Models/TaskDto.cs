using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class TaskDto
    {
        [Required]
        [MaxLength(40,ErrorMessage = "Maximum length is 40")]
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
