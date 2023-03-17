using app.Data.Interfaces;
using app.Data.Models;

namespace app.Data.Implements
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext context) : base(context)
        {
        }
    }
}