using app.Data.Interfaces;
using app.Data.Models;
using app.Interfaces;
using app.Models.DTOs;
using app.Shared;

namespace app.Services
{
    public class ForumService : IForumService
    {
        private IRepositoryManager _repository;
        private IFileService _fileService;

        public ForumService(IRepositoryManager repositoryManager, IFileService fileService)
        {
            _repository = repositoryManager;
            _fileService = fileService;
        }

        public async Task<Forum> Create(ForumDTO forumDto)
        {
            var entity = _repository.Forum.Create(new Forum
            {
                Title = forumDto.Title,
                SectionId = forumDto.SectionId,
                OrderNumber = forumDto.OrderNumber
            });

            if(forumDto.Image != null && forumDto.Image.Length > 0)
                entity.ImagePath = await _fileService.SaveFile(forumDto.Image, "images/forums");

            await _repository.SaveAsync();

            return entity;
        }

        public async Task Delete(int forumId)
        {
            var entity = await _repository.Forum.GetByIdAsync(forumId, false);

            if(entity == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError, $"No forum with id: {forumId}");

            _repository.Forum.Delete(entity);

            if(!string.IsNullOrEmpty(entity.ImagePath))
                _fileService.DeleteFile(entity.ImagePath, "images/forums");

            await _repository.SaveAsync();
        }

        public async Task Update(int forumId, ForumDTO forumDto)
        {
            var entity = await _repository.Forum.GetByIdAsync(forumId, false);
            if(entity == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.InternalServerError, $"No forum with id: {forumId}");

            entity.Title = forumDto.Title;
            entity.SectionId = forumDto.SectionId;
            entity.OrderNumber = forumDto.OrderNumber;

            if(forumDto.Image != null && forumDto.Image.Length > 0)
            {
                if(!string.IsNullOrEmpty(entity.ImagePath))
                    _fileService.DeleteFile(entity.ImagePath, "images/forums");

                entity.ImagePath = await _fileService.SaveFile(forumDto.Image, "images/forums");
            }

            await _repository.SaveAsync();
        }
    }
}