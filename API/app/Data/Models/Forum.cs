namespace app.Data.Models
{
    public partial class Forum
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int? OrderNumber { get; set; }

        public int SectionId { get; set; }

        public string? ImagePath { get; set; }

        public virtual Section Section { get; set; } = null!;

        public virtual ICollection<Topic> Topics { get; } = new List<Topic>();
    }
}