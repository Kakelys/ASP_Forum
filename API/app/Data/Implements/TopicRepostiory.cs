using app.Data.Interfaces;
using app.Data.Models;

namespace app.Data.Implements
{
    public class TopicRepostiory : RepositoryBase<Topic>, ITopicRepository
    {
        public TopicRepostiory(RepositoryContext context) : base(context)
        {
        }
    }
}