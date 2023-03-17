using app.Data.Interfaces;
using app.Data.Models;

namespace app.Data.Implements
{
    public class ForumRepository : RepositoryBase<Forum>, IForumRepository
    {
        public ForumRepository(RepositoryContext context) : base(context)
        {
        }
    }
}