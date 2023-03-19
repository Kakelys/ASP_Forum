namespace app.Models.DTOs
{
    public class JwtDTO
    {
        public string? AccessToken { get;set; }
        public string? RefreshToken { get;set; }

        public JwtDTO() {}
        public JwtDTO(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}