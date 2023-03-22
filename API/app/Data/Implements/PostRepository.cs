using app.Data.Interfaces;
using app.Data.Models;
using app.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace app.Data.Implements
{
    public class PostRepository : RepositoryBase<Post>, IPostRepository
    {
        public PostRepository(RepositoryContext context) : base(context)
        {
        }

        public Task<Post?> GetById(int postId, bool asTracking = true)
        {
            return FindByCondition(p => p.Id == postId, asTracking).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PostDTO>> GetByPage(int topicId, int page, int amountToTake)
        {
            return await FindByCondition(p => p.TopicId == topicId, false)
                .Include(p => p.Author)
                .OrderBy(p => p.CreateDate)
                .Skip(((page - 1) * amountToTake) + 1) // +1 because first post loaded with topic
                .Take(amountToTake)
                .Select(p => new PostDTO
                {
                    Id = p.Id,
                    TopicId = p.TopicId,
                    Content = p.Content,
                    CreateDate = p.CreateDate,
                    Author = new AuthorDTO
                    {
                        Id = p.AuthorId,
                        Username = p.Author.Username,
                        ImagePath = p.Author.ImagePath
                    }
                }).ToListAsync();
        }

        public async Task<Post?> GetFirstByTopicId(int topicId, bool asTracking = true)
        {
            return await FindByCondition(p => p.TopicId == topicId, asTracking)
                .OrderBy(p => p.CreateDate)
                .FirstOrDefaultAsync();
        }
    }
}