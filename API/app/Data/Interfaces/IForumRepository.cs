using app.Data.Models;

namespace app.Data.Interfaces
{
    public interface IForumRepository : IRepositoryBase<Forum>
    {
        Task<Forum?> GetByIdAsync(int forumId, bool asTracking = true);       
    }
}