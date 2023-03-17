using app.Data.Interfaces;
using app.Data.Models;

namespace app.Data.Implements
{
    public class SectionRepository : RepositoryBase<Section>, ISectionRepository
    {
        public SectionRepository(RepositoryContext context) : base(context)
        {
        }
    }
}