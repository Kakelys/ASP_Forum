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
        private readonly IConfiguration _config;

        public TopicService(IRepositoryManager repositoryManager, IConfiguration config)
        {
            _repositoryManager = repositoryManager;
            _config = config;
        }

        public async Task Create(TopicDTO topicDto)
        {
            var topic = new Topic
            {
                Title = topicDto.Title,
                ForumId = topicDto.ForumId,
                AuthorId = topicDto.Author.Id,
            };

            _repositoryManager.Topic.Create(topic);

            await _repositoryManager.SaveAsync();
        }

        public async Task Update(int senderId, TopicDTO topicDto)
        {
            var entity = await _repositoryManager.Topic.GetByIdAsync(topicDto.Id, true);
            CheckTopicIsNull(entity);
            await CheckUserPermission(senderId, entity.AuthorId);

            if(entity.IsClosed && topicDto.IsClosed == true)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest, _config["Messages:Topic:Closed"]);

            entity.Title = topicDto.Title ?? entity.Title;
            entity.IsClosed = topicDto.IsClosed;
            entity.IsPinned = topicDto.IsPinned;
            entity.ForumId = topicDto.ForumId;

            await _repositoryManager.SaveAsync();
        }

        public async Task Delete(int senderId, int topicId)
        {
            var entity = await _repositoryManager.Topic.GetByIdAsync(topicId, false);
            CheckTopicIsNull(entity);
            await CheckUserPermission(senderId, entity.AuthorId);

            _repositoryManager.Topic.Delete(entity);

            await _repositoryManager.SaveAsync();
        }

        public async Task<TopicDetailDTO> GetWithFirstPost(int topicId)
        {
            var entity = await _repositoryManager.Topic.GetWithFirstPostAsync(topicId);
            CheckTopicIsNull(entity);

            return entity;
        }

        public async Task<IEnumerable<TopicDetailDTO>> GetByPage(int forumId, int page, int amountToTake)
        {
            return await _repositoryManager.Topic.GetByPageAsync(page, amountToTake, forumId);
        }

        private void CheckTopicIsNull(object? obj)
        {
            if(obj == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest, _config["Messages:Topic:NotFound"]);
        }

        private async Task CheckUserPermission(int senderId, int topicAuthorId)
        {
            var user = await _repositoryManager.Account.GetWithRoleById(senderId, false);
            if(user == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden, _config["Messages:Topic:Forbidden"]);

            var adminRoles = _config.GetSection("Permissions:Topic").Get<List<string>>() ?? new List<string>();

            if(adminRoles.Contains(user.Role.Name))
                return;

            if(senderId != topicAuthorId)
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden, _config["Messages:Topic:Forbidden"]);
        }   
    }
}