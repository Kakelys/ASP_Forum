namespace app.Models.DTOs
{
    public class ForumDetailDTO : ForumDTO
    {
        public int PostsCount { get; set; }
        public int TopicsCount { get; set; }
        public string? ImagePath { get; set; }
        public TopicDTO? LastTopic { get;set; } = null!;
    }
}