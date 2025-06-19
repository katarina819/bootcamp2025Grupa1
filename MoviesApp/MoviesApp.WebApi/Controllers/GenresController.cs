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

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }
        [HttpGet]
        public async Task<IList<object>> GetAllGenreAsync() 
        { 
            return await _genreService.GetAllGenreAsync();
        }

        [HttpDelete]
        public async Task DeleteGenreAsync(Guid id) 
        { 
            await _genreService.DeleteGenreAsync (id);
        }

        [HttpPut]
        public async Task UpdateGenreAsync(Genre genre)
        {
            await _genreService.UpdateGenreAsync (genre);
        }

        [HttpPost]
        public async Task AddGenreAsync(Genre genre)
        {
            await _genreService.AddGenreAsync (genre);
        }

        [HttpGet ("get-genre-by-id")] 
        public async Task<Genre> GetGenreByIdAsync(Guid id)
        {
            return await _genreService.GetGenreByIdAsync (id);


        }


    }
}

