namespace app.Models.DTOs
{
    public class ForumDetailDTO : ForumDTO
    {
        public int PostCount { get; set; }
        public int TopicCount { get; set; }
        public string? ImagePath { get; set; }
        public TopicDTO? LastTopic { get;set; } = null!;
    }
}