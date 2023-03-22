using System.ComponentModel.DataAnnotations;

namespace app.Models.DTOs
{
    public class PageDTO
    {
        [Range( 1, int.MaxValue, ErrorMessage = "Valid id is required")]
        public int Id { get; set; }
        [Range( 1, int.MaxValue, ErrorMessage = "Valid page is required")]
        public int Page { get;set; }
        [Range(10, 100, ErrorMessage = "You must take between 10 and 100 posts")]
        public int Take { get;set; } = 10;
    }
}