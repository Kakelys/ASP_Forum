using app.Interfaces;
using app.Models.DTOs;
using app.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/v1/sections")]
    public class SectionController : BaseApiController
    {
        private ISectionService _sectionService;

        public SectionController(ISectionService serctionService)
        {
            _sectionService = serctionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDetail()
        {
            return Ok(await _sectionService.GetAllDetail());
        }

        [Authorize]
        [AuthorizeRoles(Role.Admin)]
        [HttpPost]
        public async Task<IActionResult> Create(SectionDTO sectionDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _sectionService.Create(sectionDto);
            return NoContent();
        }

        [Authorize]
        [AuthorizeRoles(Role.Admin)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _sectionService.Delete(id);
            return NoContent();
        }
        
        [Authorize]
        [AuthorizeRoles(Role.Admin)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SectionDTO sectionDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _sectionService.Update(id, sectionDto);
            return NoContent();
        }
    }
}