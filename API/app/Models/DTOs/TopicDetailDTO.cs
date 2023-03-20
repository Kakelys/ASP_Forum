namespace app.Models.DTOs
{
    public class TopicDetailDTO : TopicDTO
    {
        public PostDTO? LastPost { get;set; }
    }
}