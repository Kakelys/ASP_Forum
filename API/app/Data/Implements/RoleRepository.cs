using app.Data.Interfaces;
using app.Data.Models;

namespace app.Data.Implements
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(RepositoryContext context) : base(context)
        {
        }
    }
}