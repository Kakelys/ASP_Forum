using app.Data.Interfaces;
using app.Data.Models;
using app.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace app.Data.Implements
{
    public class TopicRepostiory : RepositoryBase<Topic>, ITopicRepository
    {
        public TopicRepostiory(RepositoryContext context) : base(context)
        {
            
        }

        public async Task<TopicDetailDTO?> GetWithFirstPostAsync(int topicId, bool asTracking = true){
            return await FindByCondition(t => t.Id == topicId, asTracking)
            .Include(t => t.Posts)
            .ThenInclude(p => p.Author)
            .Select(t => new TopicDetailDTO{
                Id = t.Id,
                ForumId = t.ForumId,
                Title = t.Title,
                CreateDate = t.CreateDate,
                Author = new AuthorDTO
                {
                    Id = t.AuthorId,
                    Username = t.Author.Username,
                    ImagePath = t.Author.ImagePath
                },
                FirstPost = t.Posts
                    .OrderBy(p => p.CreateDate)
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
                    }).FirstOrDefault()
            }).FirstOrDefaultAsync();
        }        

        public async Task<IEnumerable<TopicDetailDTO>> GetByPageAsync(int page, int amountToTake, int forumId)
        {
            return await context.Topics
            .AsNoTracking()
            .Include(t => t.Posts)
            .ThenInclude(p => p.Author)
            .OrderBy(t => t.IsPinned)
            .ThenByDescending(t => t.CreateDate)
            .Skip((page - 1) * amountToTake)
            .Take(amountToTake)
            .Select(t => new TopicDetailDTO
            {
                Id = t.Id,
                ForumId = t.ForumId,
                Title = t.Title,
                CreateDate = t.CreateDate,
                Author = new AuthorDTO
                {
                    Id = t.AuthorId,
                    Username = t.Author.Username,
                    ImagePath = t.Author.ImagePath
                },
                LastPost = t.Posts
                    .OrderByDescending(p => p.CreateDate)
                    .Select(p => new PostDTO
                    {
                        Id = p.Id,
                        TopicId = p.TopicId,
                        Author = new AuthorDTO
                        {
                            Id = p.AuthorId,
                            Username = p.Author.Username,
                            ImagePath = p.Author.ImagePath
                        },
                        CreateDate = p.CreateDate,
                        Content = p.Content
                    }).FirstOrDefault()
            }).ToListAsync();
        }

        public async Task<Topic?> GetByIdAsync(int topicId, bool asTracking = true)
        {
            return await context.Topics.FirstOrDefaultAsync(t => t.Id == topicId);
        }
    }
}