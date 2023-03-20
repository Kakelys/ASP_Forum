using app.Data.Interfaces;
using app.Data.Models;
using app.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace app.Data.Implements
{
    public class ForumRepository : RepositoryBase<Forum>, IForumRepository
    {
        public ForumRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<Forum?> GetByIdAsync(int forumId, bool asTracking = true)
        {
            return await FindByCondition(f => f.Id == forumId, asTracking).FirstOrDefaultAsync();
        }
    }
}