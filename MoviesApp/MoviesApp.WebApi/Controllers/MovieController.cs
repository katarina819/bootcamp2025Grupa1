using Microsoft.AspNetCore.Mvc;
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
    }
}

