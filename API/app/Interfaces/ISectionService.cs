using app.Data.Models;
using app.Models.DTOs;

namespace app.Interfaces
{
    public interface ISectionService
    {
        Task<Section> Create(SectionDTO sectionDto);
        Task Delete(int sectionId);
        Task Update(int sectionId, SectionDTO sectionDto);
        Task<IEnumerable<SectionDetailDTO>> GetAllDetail();
    }
}