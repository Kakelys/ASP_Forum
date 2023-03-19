namespace app.Models.DTOs
{
    public class ForumDetailDTO
    {
        public int Id { get;set; }
        public int SectionId { get; set; }
        public string Title { get; set; }
        public int OrderNumber { get; set; }
        public int PostsCount { get; set; }
        public int TopicsCount { get; set; }
        public string? ImagePath { get; set; }
        
    }
}