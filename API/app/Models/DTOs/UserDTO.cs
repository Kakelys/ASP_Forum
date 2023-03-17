namespace app.Models.DTOs
{
    public class UserDTO
    {
        public string? Username { get;set; }
        public JwtDTO? Jwt { get;set; }

        public UserDTO() {}
        public UserDTO(string username, JwtDTO jwt)
        {
            Username = username;
            Jwt = jwt;
        }

        public UserDTO(string username, string accessToken, string refreshToken)
        {
            Username = username;
            Jwt = new JwtDTO(accessToken, refreshToken);
        }
    }
}