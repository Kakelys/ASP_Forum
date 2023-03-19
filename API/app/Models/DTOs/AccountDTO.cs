using app.Data.Models;

namespace app.Models.DTOs
{
    public class AccountDTO
    {
        public int Id {get;set;}
        public string Username { get;set; }
        public Role Role { get;set; }

        public AccountDTO(Account account)
        {
            Id = account.Id;
            Username = account.Username;
            Role = account.Role;
        }
    }
}