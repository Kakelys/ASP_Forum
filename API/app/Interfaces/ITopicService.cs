using app.Models.DTOs;

namespace app.Interfaces
{
    public interface ITopicService
    {
        Task Create(TopicDTO topicDto);
        Task Update(int senderId, TopicDTO topicDto);
        Task Delete(int senderId, int topicId);
        Task<TopicDTO> GetById(int topicId);
        Task<IEnumerable<TopicDetailDTO>> GetByPage(int forumId, int page, int amountToTake);
    }
}