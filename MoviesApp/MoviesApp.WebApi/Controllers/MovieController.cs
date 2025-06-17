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
        public async Task DeleteMovieAsync(Guid id)
        {
            await _movieService.DeleteMovieAsync(id);
        }

        [HttpPut("update-movie")]
        public async Task UpdateMovieAsync(Movie movie)
        {
            await _movieService.UpdateMovieAsync(movie);
        }

        [HttpPost("add-movie")]
        public async Task AddMovieAsync(Movie movie)
        {
            await _movieService.AddMovieAsync(movie);
        }

        [HttpGet("get-movie-by-id")]
        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            return await _movieService.GetMovieByIdAsync(id);
        }
    }
}

