using app.Data.Models;

namespace app.Models.DTOs
{
    public class UserDTO
    {
        public AuthorDTO User {get;set;}
        public JwtDTO? Jwt { get;set; }

        public UserDTO() {}
        public UserDTO(Account user, JwtDTO jwt)
        {
            User = new AuthorDTO
            {
                Id = user.Id,
                Username = user.Username,
                ImagePath = user.ImagePath
            };
            Jwt = jwt;
        }
    }
}