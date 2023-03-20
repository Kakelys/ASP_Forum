using System.Net;
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
        private readonly ITopicService _topicService;

        public ForumController(IForumService forumService, ITopicService topicService)
        {
            _forumService = forumService;
            _topicService = topicService;
        }

        [HttpGet("{id}/pages/{page}")]
        public async Task<IActionResult> GetPage(int id, int page)
        {
            var toTake = Convert.ToInt32(Request.Query["toTake"].FirstOrDefault() ?? "10");

            return Ok(await _topicService.GetByPage(id, page, toTake));
        }

        [Authorize]
        [AuthorizeRoles(Role.Admin, Role.Moderator)]
        [HttpPost]
        public async Task<IActionResult> AddForum([FromForm] ForumDTO forumDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _forumService.Create(forumDto));
        }

        [Authorize]
        [AuthorizeRoles(Role.Admin, Role.Moderator)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForum(int id, [FromForm] ForumDTO forumDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

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