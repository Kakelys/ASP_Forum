using app.Models.DTOs;

namespace app.Interfaces
{
    public interface ITopicService
    {
        Task Create(int senderId, TopicCreateDTO topicDto);
        Task Update(int senderId, TopicDTO topicDto);
        Task Delete(int senderId, int topicId);
        Task<TopicDetailDTO> GetWithFirstPost(int topicId);
        Task<IEnumerable<TopicDetailDTO>> GetByPage(int forumId, int page, int amountToTake);
    }
}