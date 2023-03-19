using System.ComponentModel.DataAnnotations;

namespace app.Models.DTOs
{
    public class ForumDTO
    {
        public int Id { get; set; }
        public int SectionId { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string? Title { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Order number must be greater than 0")]
        public int OrderNumber { get; set; }
        public IFormFile? Image { get;set; } = null!;
    }
}