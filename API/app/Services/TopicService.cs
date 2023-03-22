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

        public TopicService(IRepositoryManager repositoryManager, IConfiguration config)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task Create(TopicCreateDTO topicDto)
        {
            await _repositoryManager.BeginTransactionAsync();
            var topic = new Topic
            {
                Title = topicDto.Title,
                ForumId = topicDto.ForumId,
                AuthorId = topicDto.AuthorId,
            };

            try{
                var entity = _repositoryManager.Topic.Create(topic);
                await _repositoryManager.SaveAsync();

                var post = new Post
                {
                    Content = topicDto.Content,
                    AuthorId = topicDto.AuthorId,
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
            var entity = await _repositoryManager.Topic.GetByIdAsync(topicDto.Id, true);
            CheckTopicIsNull(entity);
            await CheckUserPermission(senderId, entity.AuthorId);

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
                throw new HttpResponseException(System.Net.HttpStatusCode.BadRequest, "Topic not found");
        }

        private async Task CheckUserPermission(int senderId, int topicAuthorId)
        {
            var message =  "You are not permitted to do this";
            var user = await _repositoryManager.Account.GetWithRoleById(senderId, false);
            if(user == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden, message);

            var adminRoles = Shared.Role.GenerateRoleList(Shared.Role.Admin, Shared.Role.Moderator);

            if(adminRoles.Contains(user.Role.Name))
                return;

            if(senderId != topicAuthorId)
                throw new HttpResponseException(System.Net.HttpStatusCode.Forbidden, message);
        }   
    }
}