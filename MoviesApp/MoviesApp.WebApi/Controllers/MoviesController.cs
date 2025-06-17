using Microsoft.AspNetCore.Mvc;
using MoviesAppModel;
using MoviesAppService;

namespace MoviesAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<List<MovieModels>>> Get()
        {
            var movies = await _movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieModels>> Get(Guid id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            if (movie == null)
                return NotFound();
            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MovieModels movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _movieService.AddMovieAsync(movie);
            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] MovieModels movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != movie.Id)
                return BadRequest("ID mismatch.");

            var existing = await _movieService.GetMovieByIdAsync(id);
            if (existing == null)
                return NotFound();

            var updated = await _movieService.UpdateMovieAsync(movie);
            if (!updated)
                return StatusCode(500, "Failed to update movie.");

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var deleted = await _movieService.DeleteMovieAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
