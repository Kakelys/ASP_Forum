namespace app.Data.Models
{
    public partial class Token
    {
        public int Id { get; set; }
        public int AccountId { get; set; }

        public string TokenStr { get; set; } = null!;

        public DateTime ExpireDate { get; set; }

        public virtual Account Account { get; set; } = null!;
    }
}
