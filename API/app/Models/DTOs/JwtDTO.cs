using System.ComponentModel.DataAnnotations;

namespace app.Models.DTOs
{
    public class JwtDTO
    {
        public string? AccessToken { get;set; }
        [Required]
        public string RefreshToken { get;set; }

        public JwtDTO(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}