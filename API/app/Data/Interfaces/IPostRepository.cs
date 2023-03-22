using app.Data.Models;
using app.Models.DTOs;

namespace app.Data.Interfaces
{
    public interface IPostRepository : IRepositoryBase<Post>
    {
        Task<IEnumerable<PostDTO>> GetByPage(int topicId, int page, int amountToTake);    
        Task<Post?> GetFirstByTopicId(int topicId, bool asTracking = true);    
        Task<Post?> GetById(int postId, bool asTracking = true);    
    }
}