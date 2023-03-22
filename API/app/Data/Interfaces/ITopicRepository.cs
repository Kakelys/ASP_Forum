using app.Data.Models;
using app.Models.DTOs;

namespace app.Data.Interfaces
{
    public interface ITopicRepository : IRepositoryBase<Topic>
    {
        Task<IEnumerable<TopicDetailDTO>> GetByPageAsync(int page, int amountToTake, int forumId);
        Task<TopicDetailDTO?> GetWithFirstPostAsync(int topicId, bool asTracking = true);
        Task<Topic?> GetByIdAsync(int topicId, bool asTracking = true);
    }
}