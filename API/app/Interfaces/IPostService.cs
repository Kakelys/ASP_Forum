using app.Models.DTOs;

namespace app.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetByPage(int topicId, int page, int amountToTake);
        Task Create(int senderId, PostDTO postDto);
        Task Update(int senderId, PostDTO postDto);
        Task Delete(int senderId, int postId);
    }
}