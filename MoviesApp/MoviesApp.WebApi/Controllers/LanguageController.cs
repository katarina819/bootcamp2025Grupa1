using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Model;
using MoviesApp.Service.Common;

namespace MoviesApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LanguageController : ControllerBase
    {
        private readonly ILanguageService _service;

        /// <summary>
        /// Constructor for LanguageController.
        /// </summary>
        /// <param name="service">Injected language service.</param>
        public LanguageController(ILanguageService service) => _service = service;

        /// <summary>
        /// Retrieves all languages.
        /// </summary>
        /// <returns>HTTP 200 with list of languages.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        /// <summary>
        /// Retrieves a language by its unique identifier.
        /// </summary>
        /// <param name="id">Language unique identifier.</param>
        /// <returns>HTTP 200 with the language if found; otherwise 404.</returns>
        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var lang = await _service.GetByIdAsync(id);
            return lang == null ? NotFound() : Ok(lang);
        }

        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="language">Language entity to create.</param>
        /// <returns>HTTP 201 with location of created language.</returns>
        [HttpPost]
        public async Task<IActionResult> Create(Language language)
        {
            language.Id = Guid.NewGuid();
            await _service.CreateAsync(language);
            return CreatedAtAction(nameof(GetById), new { id = language.Id }, language);
        }

        /// <summary>
        /// Updates an existing language.
        /// </summary>
        /// <param name="id">Unique identifier of the language to update.</param>
        /// <param name="language">Updated language entity.</param>
        /// <returns>HTTP 204 on success; 400 if id mismatch.</returns>
        [HttpPut("id")]
        public async Task<IActionResult> Update(Guid id, Language language)
        {
            if (id != language.Id) return BadRequest();
            await _service.UpdateAsync(language);
            return NoContent();
        }

        /// <summary>
        /// Deletes a language by its unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the language to delete.</param>
        /// <returns>HTTP 204 on success.</returns>
        [HttpDelete("id")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
