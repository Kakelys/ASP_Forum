using System.ComponentModel.DataAnnotations;

namespace app.Models.DTOs
{
    public class AuthorDTO
    {
        [Range(1, int.MaxValue, ErrorMessage = "Wrong author id")]
        public int Id { get; set; }
        public string Username { get; set; }
        public string? ImagePath { get; set; }
    }
}