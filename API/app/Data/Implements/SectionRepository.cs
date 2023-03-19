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
                        PostsCount = f.Topics.Sum(t => t.Posts.Count),
                        TopicsCount = f.Topics.Count,
                        ImagePath = f.ImagePath
                    })
                }).ToListAsync();
        }
    }
}