using app.Data.Models;
using app.Models.DTOs;

namespace app.Data.Interfaces
{
    public interface ISectionRepository : IRepositoryBase<Section>
    {
        Task<IEnumerable<SectionDetailDTO>> GetDetailAsync();

        Task<Section?> GetByIdAsync(int id, bool asTracking = true);
    }
}