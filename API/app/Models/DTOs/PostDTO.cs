namespace app.Models.DTOs
{
    public class PostDTO
    {
        public int Id { get; set; }
        public int TopicId { get; set; }
        public string Content { get; set; }
        public DateTime CreateDate { get; set; }
        public AuthorDTO Author { get; set; }        
    }
}