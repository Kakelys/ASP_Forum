using System.ComponentModel.DataAnnotations;

namespace app.Models.DTOs
{
    public class TopicDTO
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Wrong forum id")]
        public int ForumId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get;set; }
        public bool IsPinned { get; set; }
        public bool IsClosed { get; set; }
        public DateTime CreateDate { get;set; }
        [Required(ErrorMessage = "Author is required")]
        public AuthorDTO? Author { get; set; }
    }
}