using app.Extensions;
using app.Interfaces;
using app.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/v1/posts")]
    public class PostController : BaseApiController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }  
        
        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PageDTO pageDto)
        {
            return Ok(await _postService.GetByPage(pageDto.Id, pageDto.Page, pageDto.Take));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(PostDTO postDto)
        {
            await _postService.Create(HttpContext.GetUserId(), postDto);

            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, PostDTO postDto)
        {
            postDto.Id = id;
            await _postService.Update(HttpContext.GetUserId(), postDto);

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            await _postService.Delete(HttpContext.GetUserId(), id);

            return NoContent();
        }
    }
}