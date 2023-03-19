using app.Data.Models;
using app.Models.DTOs;

namespace app.Interfaces
{
    public interface IForumService
    {
        Task<Forum> Create(ForumDTO forumDto);
        Task Delete(int forumId);
        Task Update(int forumId, ForumDTO forumDto);
    }
}