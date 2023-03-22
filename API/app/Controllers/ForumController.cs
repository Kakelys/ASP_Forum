using app.Interfaces;
using app.Models.DTOs;
using app.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/v1/forums")]
    public class ForumController : BaseApiController
    {
        private readonly IForumService _forumService;

        public ForumController(IForumService forumService, ITopicService topicService)
        {
            _forumService = forumService;
        }

        [Authorize]
        [AuthorizeRoles(Role.Admin, Role.Moderator)]
        [HttpPost]
        public async Task<IActionResult> AddForum([FromForm] ForumDTO forumDto)
        {
            return Ok(await _forumService.Create(forumDto));
        }

        [Authorize]
        [AuthorizeRoles(Role.Admin, Role.Moderator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForum(int id, [FromForm] ForumDTO forumDto)
        {
            await _forumService.Update(id, forumDto);
            return NoContent();
        }

        [Authorize]
        [AuthorizeRoles(Role.Admin, Role.Moderator)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForum(int id)
        {
            await _forumService.Delete(id);
            return NoContent();
        }
    }
}