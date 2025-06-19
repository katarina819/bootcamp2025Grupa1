using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Model;
using MoviesApp.Service.Common;

namespace MoviesApp.Controllers
{
    [ApiController]
    [Route("id")]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _service;
        public LanguageController(ILanguageService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lang = await _service.GetByIdAsync(id);
            return lang == null ? NotFound() : Ok(lang);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Language language)
        {
            language.Id = Guid.NewGuid();
            await _service.CreateAsync(language);
            return CreatedAtAction(nameof(GetById), new { id = language.Id }, language);
        }

        [HttpPut("id")]
        public async Task<IActionResult> Update(Guid id, Language language)
        {
            if (id != language.Id) return BadRequest();
            await _service.UpdateAsync(language);
            return NoContent();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
