namespace app.Models.DTOs
{
    public class TopicDetailDTO : TopicDTO
    {
        public PostDTO? FirstPost { get;set; }
        public PostDTO? LastPost { get;set; }
    }
}