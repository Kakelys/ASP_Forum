namespace app.Data.Models
{
    public partial class Topic
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public DateTime? CreateDate { get; set; }

        public bool? IsPinned { get; set; }

        public bool? IsClosed { get; set; }

        public int AuthorId { get; set; }

        public int ForumId { get; set; }

        public virtual Account Author { get; set; } = null!;

        public virtual Forum Forum { get; set; } = null!;

        public virtual ICollection<Post> Posts { get; } = new List<Post>();
    }
}
