namespace app.Data.Models
{

    public partial class Role
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public int RightLevel { get; set; }

        public virtual ICollection<Account> Accounts { get; } = new List<Account>();
    }
}