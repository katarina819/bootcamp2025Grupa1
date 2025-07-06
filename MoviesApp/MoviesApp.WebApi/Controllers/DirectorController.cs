using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
using MoviesApp.Service.Common;

namespace MoviesApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DirectorController : ControllerBase
    {
        private readonly IDirectorService _directorService;

        /// <summary>
        /// Constructor for DirectorController.
        /// </summary>
        /// <param name="directorService">Injected director service.</param>
        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
        }

        /// <summary>
        /// Retrieves a paginated list of directors.
        /// </summary>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Page size (default is 1).</param>
        /// <returns>Paginated list of directors.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var directors = await _directorService.GetAllDirectorsAsync(page, pageSize);
            return Ok(directors);
        }

        /// <summary>
        /// Retrieves a director by their unique identifier.
        /// </summary>
        /// <param name="id">Director's unique identifier.</param>
        /// <returns>The director data if found; 404 Not Found otherwise.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var director = await _directorService.GetDirectorByIdAsync(id);
            if (director == null) return NotFound();
            return Ok(director);
        }

        /// <summary>
        /// Creates a new director.
        /// </summary>
        /// <param name="dto">Director data transfer object.</param>
        /// <returns>Created director with 201 status code.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DirectorCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdDirector = await _directorService.CreateDirectorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdDirector.Id }, createdDirector);

        }

        /// <summary>
        /// Updates an existing director.
        /// </summary>
        /// <param name="id">Director's unique identifier.</param>
        /// <param name="dto">Updated director data.</param>
        /// <returns>NoContent if updated; 404 Not Found if director does not exist.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DirectorCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _directorService.UpdateDirectorAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        /// <summary>
        /// Deletes a director by their unique identifier.
        /// </summary>
        /// <param name="id">Director's unique identifier.</param>
        /// <returns>NoContent if deleted; 404 Not Found if director does not exist.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _directorService.DeleteDirector(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
