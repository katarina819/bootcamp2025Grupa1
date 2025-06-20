using Microsoft.AspNetCore.Mvc;
using MoviesApp.DTO;
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

        [HttpGet("get-movies-sorted")]
        public async Task<ActionResult<IList<Movie>>> GetMoviesSortedAsync(string? sortBy, string? sortOrder)
        {
            var validColumns = new HashSet<string>
            {
                "Name",
                "Duration",
                "Rating",
                "ReleaseYear",
                "Description"
            };
            var order = sortOrder?.ToLower() == "desc" ? "DESC" : "ASC";
            if (string.IsNullOrWhiteSpace(sortBy) || !validColumns.Contains(sortBy))
            {
                return BadRequest("This column doesn't exist.");
            }
            return await _movieService.GetMoviesSortedAsync(sortBy, order);
        }

        [HttpGet("get-movies-filter-name")]
        public async Task<ActionResult<List<Movie>>> GetMoviesFilterNameAsync(string filter)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return BadRequest("No filter.");
            }
            var filterLower = "%" + filter.ToLower() + "%";
            return await _movieService.GetMoviesFilterNameAsync(filterLower);
        }

        [HttpGet("get-genres-by-movie-id")]
        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            return await _movieService.GetGenresByMovieIdAsync(movieId);
        }

        [HttpGet("get-languages-by-movie-id")]
        public async Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId)
        {
            return await _movieService.GetLanguagesByMovieIdAsync(movieId);
        }

        [HttpGet("get-director-by-movie-id")]

        public async Task<DirectorCreateDto> GetDirectorByMovieIdAsync(Guid movieId)
        {
            return await _movieService.GetDirectorByMovieIdAsync(movieId);
        }
        
        [HttpGet("get-movie-details")]

        public async Task<MovieDetailsDto> GetMovieDetails(Guid id)
        {
            var movie = await _movieService.GetMovieByIdAsync(id);
            var movieGenres = await _movieService.GetGenresByMovieIdAsync(id);
            var movieLanguages = await _movieService.GetLanguagesByMovieIdAsync(id);
            var movieDirector = await _movieService.GetDirectorByMovieIdAsync(id);

            return new MovieDetailsDto
            {
                Name = movie.Name,
                Duration = movie.Duration,
                Rating = movie.Rating,
                ReleaseYear = movie.ReleaseYear,
                Description = movie.Description,
                DirectorName = movieDirector.FirstName + " " + movieDirector.LastName,
                Genres = movieGenres,
                Languages = movieLanguages
            };

        }
    }
}

