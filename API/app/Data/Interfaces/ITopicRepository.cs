using app.Data.Models;
using app.Models.DTOs;

namespace app.Data.Interfaces
{
    public interface ITopicRepository : IRepositoryBase<Topic>
    {
        Task<IEnumerable<TopicDetailDTO>> GetByPage(int page, int amountToTake, int forumId);
        Task<TopicDetailDTO?> GetWithFirstPost(int topicId, bool asTracking = true);
        Task<Topic?> GetById(int topicId, bool asTracking = true);
    }
}