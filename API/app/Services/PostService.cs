using app.Data.Interfaces;
using app.Data.Models;
using app.Interfaces;
using app.Models.DTOs;
using app.Shared;

namespace app.Services
{
    public class PostService : IPostService
    {
        private readonly IRepositoryManager _repository;
        private readonly IPermissionCheckerService _permCheckerService;

        public PostService(IRepositoryManager repositoryManager, IPermissionCheckerService permissionCheckerService)
        {
            _repository = repositoryManager;
            _permCheckerService = permissionCheckerService;
        }

        public Task<IEnumerable<PostDTO>> GetByPage(int topicId, int page, int amountToTake)
        {
            return _repository.Post.GetByPage(topicId, page, amountToTake);
        }

        public async Task Create(int senderId, PostDTO postDto)
        {
            var post = new Post
            {
                Content = postDto.Content,
                TopicId = postDto.TopicId,
                AuthorId = senderId
            };

            _repository.Post.Create(post);

            await _repository.SaveAsync();
        }

        public async Task Delete(int senderId, int id)
        {
            var entity = await _repository.Post.GetById(id, false);
            CheckPostIsNull(entity);
            await _permCheckerService.CheckUserPermission(senderId, entity.AuthorId, null, Shared.Role.Admin, Shared.Role.Moderator);

            _repository.Post.Delete(entity);

            await _repository.SaveAsync();
        }

        public async Task Update(int senderId, PostDTO postDto)
        {
            var entity = await _repository.Post.GetById(postDto.Id);
            CheckPostIsNull(entity);
            await _permCheckerService.CheckUserPermission(senderId, entity.AuthorId, null);

            entity.Content = postDto.Content;
            entity.LastEditDate = DateTime.Now;

            await _repository.SaveAsync();
        }

        private void CheckPostIsNull(Post? post)
        {
            if (post == null)
                throw new HttpResponseException(System.Net.HttpStatusCode.NotFound, "Post not found");
        }
    }
}