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
        
        public DirectorController(IDirectorService directorService)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 1)
        {
            var directors = await _directorService.GetAllDirectorsAsync(page, pageSize);
            return Ok(directors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var director = await _directorService.GetDirectorByIdAsync(id);
            if (director == null) return NotFound();
            return Ok(director);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DirectorCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var createdDirector = await _directorService.CreateDirectorAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdDirector.Id }, createdDirector);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] DirectorCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _directorService.UpdateDirectorAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _directorService.DeleteDirector(id);
            if (!deleted) return NotFound();

            return NoContent();
        }

    }
}
