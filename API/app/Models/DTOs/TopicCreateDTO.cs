using System.ComponentModel.DataAnnotations;

namespace app.Models.DTOs
{
    public class TopicCreateDTO
    {
        [Required]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 100 characters")]
        public string Title { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Valid forum id is required")]
        public int ForumId { get; set; }
        [Required]
        [StringLength(8000, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 8000 characters")]
        public string Content { get; set; }
    }
}