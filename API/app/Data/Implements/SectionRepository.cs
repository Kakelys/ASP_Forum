using System.Net.Mime;
using app.Data.Interfaces;
using app.Data.Models;
using app.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace app.Data.Implements
{
    public class SectionRepository : RepositoryBase<Section>, ISectionRepository
    {
        public SectionRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<Section?> GetByIdAsync(int id, bool asTracking = true)
        {
            return await FindByCondition(s => s.Id == id, asTracking).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SectionDetailDTO>> GetDetailAsync()
        {
            // TODO: add few thousand rows and test this query, if slow - optimize :)
            return await context.Sections
                .AsNoTracking()
                .Include(s => s.Forums)
                .ThenInclude(f => f.Topics)
                .ThenInclude(t => t.Posts)
                .OrderBy(s => s.OrderNumber)
                .Select(s => new SectionDetailDTO
                {
                    Id = s.Id,
                    Title = s.Title,
                    OrderNumber = s.OrderNumber,
                    Forums = s.Forums
                    .OrderBy(f => f.OrderNumber)
                    .Select(f => new ForumDetailDTO
                    {
                        Id = f.Id,
                        Title = f.Title,
                        SectionId = f.SectionId,
                        PostCount = f.Topics.Sum(t => t.Posts.Count),
                        TopicCount = f.Topics.Count,
                        ImagePath = f.ImagePath,
                        LastTopic = f.Topics
                            .OrderByDescending(t => t.CreateDate)
                            .Select(t => new TopicDTO
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
                                }
                            })
                            .FirstOrDefault()
                    })
                }).ToListAsync();
        }
    }
}