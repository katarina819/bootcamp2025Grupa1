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

        /// <summary>
        /// Constructor for MovieController.
        /// </summary>
        /// <param name="movieService">Injected movie service.</param>
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Retrieves paginated list of movies.
        /// </summary>
        /// <param name="page">Page number (default 1).</param>
        /// <param name="pageSize">Page size (default 10).</param>
        /// <returns>Paginated list of movies.</returns>
        [HttpGet("get-all-movies")]
        public async Task<IActionResult> GetAllMoviesAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var movies = await _movieService.GetAllMoviesAsync(page, pageSize);
            return Ok(movies);
        }

        /// <summary>
        /// Deletes a movie by its ID.
        /// </summary>
        /// <param name="id">ID of the movie to delete.</param>
        /// <returns>Ok if deleted; BadRequest if movie does not exist.</returns>
        [HttpDelete("{id}")]
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

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        /// <param name="movie">Movie data to update.</param>
        /// <returns>Ok on success.</returns>
        [HttpPut("update-movie")]
        public async Task<IActionResult> UpdateMovieAsync(MovieCreateDto movie)
        {
            await _movieService.UpdateMovieAsync(movie);
            return Ok("Movie updated.");
        }

        /// <summary>
        /// Adds a new movie with validation for duration, rating, and release year.
        /// </summary>
        /// <param name="movie">Movie data to add.</param>
        /// <returns>Ok if added; BadRequest if validation fails.</returns>
        [HttpPost("add-movie")]
        public async Task<IActionResult> AddMovieAsync(MovieCreateDto movie)
        {
            if (movie.Duration < 1 || movie.Duration > 600)
            {
                return BadRequest("Duration must be in range 1-600 minutes.");
            }

            if (movie.Rating <= 0 || movie.Rating > 10)
            {
                return BadRequest("Rating must be in range 0-10.");
            }

            if (movie.ReleaseYear < 1888 || movie.ReleaseYear > 2100)
            {
                return BadRequest("Release year must be in range 1888-2100.");
            }
            movie.Description ??= "";

            await _movieService.AddMovieAsync(movie);
            return Ok("Movie added.");
        }

        /// <summary>
        /// Retrieves a movie by its ID.
        /// </summary>
        /// <param name="id">ID of the movie.</param>
        /// <returns>The movie if found; NotFound otherwise.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovieByIdAsync(Guid id)
        {
            try
            {
                var movie = await _movieService.GetMovieByIdAsync(id);

                if (movie == null)
                    return NotFound($"Movie with id: {id} doesn't exist.");

                return Ok(movie);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Retrieves movies sorted by a specified column and order with optional genre filter.
        /// </summary>
        /// <param name="sortBy">Column to sort by (name, duration, rating, releaseyear).</param>
        /// <param name="sortOrder">Sort order (asc or desc).</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <param name="genreId">Optional genre filter.</param>
        /// <returns>Paginated sorted list of movies.</returns>
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

        /// <summary>
        /// Retrieves movies filtered by a name substring.
        /// </summary>
        /// <param name="filter">Filter substring for movie name.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Page size.</param>
        /// <returns>Paginated list of filtered movies.</returns>
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

        /// <summary>
        /// Retrieves genres associated with a movie.
        /// </summary>
        /// <param name="movieId">ID of the movie.</param>
        /// <returns>List of genre names.</returns>
        [HttpGet("{movieId}/genres")]
        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            return await _movieService.GetGenresByMovieIdAsync(movieId);
        }

        /// <summary>
        /// Retrieves languages associated with a movie.
        /// </summary>
        /// <param name="movieId">ID of the movie.</param>
        /// <returns>List of language names.</returns>
        [HttpGet("get-languages-by-movie-id")]
        public async Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId)
        {
            return await _movieService.GetLanguagesByMovieIdAsync(movieId);
        }

        /// <summary>
        /// Retrieves director information for a movie.
        /// </summary>
        /// <param name="movieId">ID of the movie.</param>
        /// <returns>Director create DTO.</returns>
        [HttpGet("get-director-by-movie-id")]
        public async Task<DirectorCreateDto> GetDirectorByIdAsync(Guid movieId)
        {
            return await _movieService.GetDirectorByIdAsync(movieId);
        }

        /// <summary>
        /// Retrieves detailed information about a movie including genres, languages, and director.
        /// </summary>
        /// <param name="id">ID of the movie.</param>
        /// <returns>Movie details DTO.</returns>
        [HttpGet("details/{id}")]
public async Task<ActionResult<MovieDetailsDto>> GetMovieDetails(Guid id)
{
    var movie = await _movieService.GetMovieByIdAsync(id);
    if (movie == null)
        return NotFound($"Movie with id: {id} doesn't exist.");

    var movieGenres = await _movieService.GetGenresByMovieIdAsync(id);
    var movieLanguages = await _movieService.GetLanguagesByMovieIdAsync(id);

    // Direktno koristimo movie.DirectorId jer nije nullable
    var movieDirector = await _movieService.GetDirectorByIdAsync(movie.DirectorId);
    string? directorName = movieDirector?.Name;

    var movieDetailsDto = new MovieDetailsDto
    {
        Id = movie.Id,
        Name = movie.Name,
        Duration = movie.Duration,
        Rating = movie.Rating,
        ReleaseYear = movie.ReleaseYear,
        Description = movie.Description,
        DirectorName = directorName,
        Genres = movieGenres,
        Languages = movieLanguages
    };

    return Ok(movieDetailsDto);
}



        /// <summary>
        /// Retrieves a list of movie names filtered by a given substring.
        /// </summary>
        /// <param name="filter">Substring filter for movie names.</param>
        /// <returns>List of movie names.</returns>
        [HttpGet("search")]
        public async Task<List<MovieNameDto>> GetFilteredMovieNamesAsync(string filter)
        {
            return await _movieService.GetFilteredMovieNamesAsync(filter);
        }
    }
}
