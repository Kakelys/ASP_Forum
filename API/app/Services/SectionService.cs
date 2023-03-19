using app.Data.Interfaces;
using app.Data.Models;
using app.Interfaces;
using app.Models.DTOs;
using app.Shared;

namespace app.Services
{
    public class SectionService : ISectionService
    {
        private IRepositoryManager _repository;

        public SectionService(IRepositoryManager repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<SectionDetailDTO>> GetAllDetail()
        {
            return _repository.Section.GetDetailAsync();
        }
        public async Task<Section> Create(SectionDTO sectionDto)
        {
            var entity = _repository.Section.Create(new Section
            {
                Title = sectionDto.Title,
                OrderNumber = sectionDto.OrderNumber
            });

            await _repository.SaveAsync();

            return entity;
        }

        public async Task Delete(int sectionId)
        {
            var entity = await _repository.Section.GetByIdAsync(sectionId);
            if(entity == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError, $"No section with id: {sectionId}");

            _repository.Section.Delete(entity);

            await _repository.SaveAsync();
        }

        public async Task Update(int sectionId, SectionDTO sectionDto)
        {
            var entity = await _repository.Section.GetByIdAsync(sectionId);
            if(entity == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError, $"No section with id: {sectionId}");

            entity.Title = sectionDto.Title;
            entity.OrderNumber = sectionDto.OrderNumber;
            await _repository.SaveAsync();
        }
    }
}