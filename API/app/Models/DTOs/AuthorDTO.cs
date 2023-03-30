using System.ComponentModel.DataAnnotations;

namespace app.Models.DTOs
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? ImagePath { get; set; }
        public string? Role {get;set; }
    }
}