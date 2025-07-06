using Microsoft.AspNetCore.Mvc;
using MoviesApp.Model;
using MoviesAppService;

namespace MoviesAppWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        /// <summary>
        /// Constructor for GenresController.
        /// </summary>
        /// <param name="genreService">Injected genre service.</param>
        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Retrieves all genres.
        /// </summary>
        /// <returns>List of genres as objects.</returns>
        [HttpGet]
        public async Task<IList<object>> GetAllGenreAsync()
        {
            return await _genreService.GetAllGenreAsync();
        }

        /// <summary>
        /// Deletes a genre by its unique identifier.
        /// </summary>
        /// <param name="id">Genre unique identifier.</param>
        [HttpDelete]
        public async Task DeleteGenreAsync(Guid id)
        {
            await _genreService.DeleteGenreAsync(id);
        }

        /// <summary>
        /// Updates an existing genre.
        /// </summary>
        /// <param name="genre">Genre entity to update.</param>
        [HttpPut]
        public async Task UpdateGenreAsync(Genre genre)
        {
            await _genreService.UpdateGenreAsync(genre);
        }

        /// <summary>
        /// Adds a new genre with the specified name.
        /// </summary>
        /// <param name="name">Name of the new genre.</param>
        [HttpPost]
        public async Task AddGenreAsync(string name)
        {
            Genre genre = new Genre
            {
                Id = Guid.NewGuid(),
                Name = name,
            };

            await _genreService.AddGenreAsync(genre);
        }

        /// <summary>
        /// Retrieves a genre by its unique identifier.
        /// </summary>
        /// <param name="id">Genre unique identifier.</param>
        /// <returns>The genre entity.</returns>
        [HttpGet("get-genre-by-id")]
        public async Task<Genre> GetGenreByIdAsync(Guid id)
        {
            return await _genreService.GetGenreByIdAsync(id);
        }

        /// <summary>
        /// Retrieves genre names associated with a specific movie.
        /// </summary>
        /// <param name="movieId">Movie unique identifier.</param>
        /// <returns>List of genre names.</returns>
        [HttpGet("get-genre-movie-by-id")]
        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            return await _genreService.GetGenresByMovieIdAsync(movieId);
        }
    }
}
