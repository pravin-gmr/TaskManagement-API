using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class TaskDetailAddModel
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "title is required")]
        [JsonPropertyName("title")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "description is required")]
        [JsonPropertyName("description")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "due date is required")]
        [JsonPropertyName("dueDate")]
        public required DateTime DueDate { get; set; }

        [Required(ErrorMessage = "status is required")]
        [JsonPropertyName("status")]
        public required string Status { get; set; }
    }
}
