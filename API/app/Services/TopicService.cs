using app.Data.Interfaces;
using app.Data.Models;
using app.Interfaces;
using app.Models.DTOs;
using app.Shared;

namespace app.Services
{
    public class TopicService : ITopicService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IPermissionCheckerService _permCheckerService;

        public TopicService(IRepositoryManager repositoryManager, IPermissionCheckerService permissionCheckerService)
        {
            _repositoryManager = repositoryManager;
            _permCheckerService = permissionCheckerService;
        }

        public async Task Create(int senderId, TopicCreateDTO topicDto)
        {
            await _repositoryManager.BeginTransactionAsync();
            var topic = new Topic
            {
                Title = topicDto.Title,
                ForumId = topicDto.ForumId,
                AuthorId = senderId,
            };

            try{
                var entity = _repositoryManager.Topic.Create(topic);
                await _repositoryManager.SaveAsync();

                var post = new Post
                {
                    Content = topicDto.Content,
                    AuthorId = senderId,
                    TopicId = entity.Id
                };

                _repositoryManager.Post.Create(post);

                await _repositoryManager.SaveAsync();
                await _repositoryManager.CommitAsync();
            }
            catch(Exception ex)
            {
                await _repositoryManager.RollbackAsync();
                throw ex;
            }
        }

        public async Task Update(int senderId, TopicDTO topicDto)
        {
            var entity = await _repositoryManager.Topic.GetById(topicDto.Id, true);
            CheckTopicIsNull(entity);
            await _permCheckerService.CheckUserPermission(senderId, entity.AuthorId, null, Shared.Role.Admin, Shared.Role.Admin);

            if(entity.IsClosed && topicDto.IsClosed == true)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest, "Topic is closed");

            entity.Title = topicDto.Title ?? entity.Title;
            entity.IsClosed = topicDto.IsClosed;
            entity.IsPinned = topicDto.IsPinned;
            entity.ForumId = topicDto.ForumId;

            await _repositoryManager.SaveAsync();
        }

        public async Task Delete(int senderId, int topicId)
        {
            var entity = await _repositoryManager.Topic.GetById(topicId, false);
            CheckTopicIsNull(entity);
            await _permCheckerService.CheckUserPermission(senderId, entity.AuthorId, null, Shared.Role.Admin, Shared.Role.Admin);

            _repositoryManager.Topic.Delete(entity);

            await _repositoryManager.SaveAsync();
        }

        public async Task<TopicDetailDTO> GetWithFirstPost(int topicId)
        {
            var entity = await _repositoryManager.Topic.GetWithFirstPost(topicId);
            CheckTopicIsNull(entity);

            return entity;
        }

        public async Task<IEnumerable<TopicDetailDTO>> GetByPage(int forumId, int page, int amountToTake)
        {
            return await _repositoryManager.Topic.GetByPage(page, amountToTake, forumId);
        }

        private void CheckTopicIsNull(object? obj)
        {
            if(obj == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest, "Topic not found");
        }
    }
}