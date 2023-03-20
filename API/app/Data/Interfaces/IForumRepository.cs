using app.Data.Models;
using app.Models.DTOs;

namespace app.Data.Interfaces
{
    public interface IForumRepository : IRepositoryBase<Forum>
    {
        Task<Forum?> GetByIdAsync(int forumId, bool asTracking = true);
    }
}