using System.ComponentModel.DataAnnotations;

namespace app.Models.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(16, MinimumLength = 3, ErrorMessage = "Username must be between 3 and 16 characters")]
        public string? Username { get;set; }
        
        [Required(ErrorMessage = "Password is required")]
        [StringLength(16, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 16 characters")]
        public string? Password { get;set; }
    }
}