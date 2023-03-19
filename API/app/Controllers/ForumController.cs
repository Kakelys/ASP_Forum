using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app.Interfaces;
using app.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/v1/forums")]
    public class ForumController : BaseApiController
    {
        private IForumService _forumService;

        public ForumController(IForumService forumService)
        {
            _forumService = forumService;
        }

        // TODO: Add Authorize
        [HttpPost]
        public async Task<IActionResult> AddForum([FromForm] ForumDTO forumDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await _forumService.Create(forumDto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateForum(int id, [FromForm] ForumDTO forumDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _forumService.Update(id, forumDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteForum(int id)
        {
            await _forumService.Delete(id);
            return NoContent();
        }
    }
}