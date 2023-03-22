using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace app.Models.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Valid topic id is required")]
        public int TopicId { get; set; }
        [Required]
        [StringLength(3000, MinimumLength = 1, ErrorMessage = "Content must be between 1 and 3000 characters")]
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public AuthorDTO? Author { get; set; }        
    }
}