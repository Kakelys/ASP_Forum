using app.Extensions;
using app.Interfaces;
using app.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/v1/topics")]
    public class TopicController : BaseApiController
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPage([FromQuery] PageDTO pageDto)
        {            
            return Ok(await _topicService.GetByPage(pageDto.Id, pageDto.Page, pageDto.Take));
        }
 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopic(int id)
        {
            return Ok(await _topicService.GetWithFirstPost(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTopic(TopicCreateDTO topicDto)
        {
            await _topicService.Create(HttpContext.GetUserId(), topicDto);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic(TopicDTO topicDto)
        {
            await _topicService.Update(HttpContext.GetUserId(), topicDto);
            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(int id)
        {
            await _topicService.Delete(HttpContext.GetUserId(), id);
            return NoContent();
        }
    }
}