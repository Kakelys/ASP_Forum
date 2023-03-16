namespace app.Data.Models
{

    public partial class Post
    {
        public int Id { get; set; }

        public string Content { get; set; } = null!;

        public DateTime? CreateDate { get; set; }

        public DateTime? LastEditDate { get; set; }

        public int AuthorId { get; set; }

        public int TopicId { get; set; }

        public virtual Account Author { get; set; } = null!;

        public virtual Topic Topic { get; set; } = null!;
    }
}