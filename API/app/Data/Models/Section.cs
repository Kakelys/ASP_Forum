namespace app.Data.Models
{
    public partial class Section
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int? OrderNumber { get; set; }

        public virtual ICollection<Forum> Forums { get; } = new List<Forum>();
    }
}
