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

        public async Task<IActionResult> GetAllMoviesAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var movies = await _movieService.GetAllMoviesAsync(page, pageSize);
            return Ok(movies);
        }

        [HttpDelete("delete-movie")]
        public async Task<IActionResult> DeleteMovieAsync(Guid id)
        {
            try
            {
                await _movieService.DeleteMovieAsync(id);
                return Ok("Movie deleted");
            }
            catch (Exception)
            {
                return BadRequest($"Movie with id: {id} doesn't exist.");
            }
        }

        [HttpPut("update-movie")]
        public async Task<IActionResult> UpdateMovieAsync(MovieCreateDto movie)
        {
            await _movieService.UpdateMovieAsync(movie);
            return Ok("Movie updated.");
        }

        [HttpPost("add-movie")]
        public async Task<IActionResult> AddMovieAsync(MovieCreateDto movie)
        {
            if (movie.Duration < 1 || movie.Duration > 600)
            {
                return BadRequest("Duration must be in range 1-600 minutes.");
            }

            if(movie.Rating <= 0 || movie.Rating > 10)
            {
                return BadRequest("Rating must be in range 0-10.");
            }

            if(movie.ReleaseYear < 1888 || movie.ReleaseYear > 2100)
            {
                return BadRequest("Release year must be in range 1888-2100.");
            }
            movie.Description ??= "";
            
            
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
            catch (Exception)
            {
                return BadRequest($"Movie with id: {id} doesn't exist.");
            }
        }

        [HttpGet("get-movies-sorted")]
        public async Task<IActionResult> GetMoviesSortedAsync(string? sortBy, string? sortOrder, int page = 1, int pageSize = 10, Guid? genreId = null)
        {
            
            var sort = sortBy?.ToLower() switch
            {
                "name" => "Name",
                "duration" => "Duration",
                "rating" => "Rating",
                "releaseyear" => "ReleaseYear",
                _ => "Name"
            };
            var order = sortOrder?.ToLower() == "desc" ? "DESC" : "ASC";
            
            var movies = await _movieService.GetMoviesSortedAsync(sort, order, page, pageSize, genreId);
            return Ok(movies);
        }

        [HttpGet("get-movies-filter-name")]
        public async Task<IActionResult> GetMoviesFilterNameAsync(string filter, int page = 1, int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(filter))
            {
                return BadRequest("No filter.");
            }
            var filterLower = "%" + filter.ToLower() + "%";
            var movies = await _movieService.GetMoviesFilterNameAsync(filterLower, page, pageSize);
            return Ok(movies);
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

        public async Task<DirectorCreateDto> GetDirectorByIdAsync(Guid movieId)
        {
            return await _movieService.GetDirectorByIdAsync(movieId);
        }
        
        [HttpGet("{id}")]

        public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(Guid id)
        {
            try
            {
                Movie movieCheck = await _movieService.GetMovieByIdAsync(id);
                
            }
            catch (Exception)
            {
                return BadRequest("Movie with id: {id} doesn't exist.");
            }
            var movie = await _movieService.GetMovieByIdAsync(id);
            var movieGenres = await _movieService.GetGenresByMovieIdAsync(id);
            var movieLanguages = await _movieService.GetLanguagesByMovieIdAsync(id);
            var movieDirector = await _movieService.GetDirectorByIdAsync(movie.DirectorId);

            return new MovieDetailsDto
            {
                Name = movie.Name,
                Duration = movie.Duration,
                Rating = movie.Rating,
                ReleaseYear = movie.ReleaseYear,
                Description = movie.Description,
                DirectorName = movieDirector.Name,
                Genres = movieGenres,
                Languages = movieLanguages
            };
        }
        [HttpGet("search")]
        public async Task<List<MovieNameDto>> GetFilteredMovieNamesAsync(string filter)
        {
            return await _movieService.GetFilteredMovieNamesAsync(filter);
        }
    }
}

