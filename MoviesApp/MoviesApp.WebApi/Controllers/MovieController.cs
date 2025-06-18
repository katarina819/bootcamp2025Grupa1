using Microsoft.AspNetCore.Mvc;
using MoviesApp.Model;
using MoviesApp.Service.Common;

namespace MoviesApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("get-all-movies")]

        public async Task<IList<object>> GetAllMoviesAsync()
        {
            return await _movieService.GetAllMoviesAsync();
        }

        [HttpDelete("delete-movie")]
        public async Task<IActionResult> DeleteMovieAsync(Guid id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return Ok("Movie deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-movie")]
        public async Task<IActionResult> UpdateMovieAsync(Movie movie)
        {
            await _movieService.UpdateMovieAsync(movie);
            return Ok("Movie updated.");
        }

        [HttpPost("add-movie")]
        public async Task<IActionResult> AddMovieAsync(string name, int duration, float rating, int releaseYear, string? description)
        {
            if (duration < 1 || duration > 600)
            {
                return BadRequest("Duration must be in range 1-600 minutes.");
            }

            if(rating <= 0 || rating > 10)
            {
                return BadRequest("Rating must be in range 0-10.");
            }

            if(releaseYear < 1888 || releaseYear > 2100)
            {
                return BadRequest("Release year must be in range 1888-2100.");
            }
            
            Movie movie = new()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Duration = duration,
                Rating = rating,
                ReleaseYear = releaseYear,
                Description = description
            };
            
            await _movieService.AddMovieAsync(movie);
            return Ok("Movie added.");
        }

        [HttpGet("get-movie-by-id")]
        public async Task<ActionResult<Movie>> GetMovieByIdAsync(Guid id)
        {
            try
            {
                return await _movieService.GetMovieByIdAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

