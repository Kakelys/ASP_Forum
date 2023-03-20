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

        // TODO: Add Authorize 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopic(int id)
        {
            return Ok(_topicService.GetById(id));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTopic(TopicDTO topicDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _topicService.Create(topicDto);
            return NoContent();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic(TopicDTO topicDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

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