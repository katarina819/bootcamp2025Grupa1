using Microsoft.AspNetCore.Mvc;
using MoviesApp.Model;
using MoviesAppService;

namespace MoviesAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenresController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFilteredGenres(
            [FromQuery] string? name,
            [FromQuery] string? sort,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var genres = await _service.GetAllAsync(name, sort, page, pageSize);
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var genre = await _service.GetByIdAsync(id);
            if (genre == null)
                return NotFound("Genre not found.");

            return Ok(genre);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Genre genre)
        {
            // 1. Model-level validacija (npr. [Required], [StringLength])
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2. Dodatna custom validacija na kontroleru
            if (genre.Name.Contains("neka zabranjena riječ", StringComparison.OrdinalIgnoreCase))
                return BadRequest("Genre name contains forbidden words.");

            try
            {
                // 3. Poziv servisnog sloja koji može baciti greške (npr. duplikat)
                await _service.AddAsync(genre);
                return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
            }
            catch (InvalidOperationException ex)
            {
                // Poslovna greška (npr. žanr već postoji)
                return Conflict(new { error = ex.Message });
            }
            catch (ArgumentException ex)
            {
                // Greška u podacima (npr. Name je prazan ili neispravan)
                return BadRequest(new { error = ex.Message });
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] Genre genre)
        {
            if (genre == null)
                return BadRequest("Genre data is required.");

            if (id != genre.Id)
                return BadRequest("Route ID and genre ID do not match.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // automatski uključuje sve validacijske poruke

            var existingGenre = await _service.GetByIdAsync(id);
            if (existingGenre == null)
                return NotFound("Genre not found.");

            await _service.UpdateAsync(genre);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }


    }
}

