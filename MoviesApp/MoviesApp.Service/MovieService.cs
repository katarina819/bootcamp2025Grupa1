using MoviesApp.Repository.Common;
using MoviesApp.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IList<object>> GetAllMoviesAsync()
        {
            return await _movieRepository.GetAllMoviesAsync();
        }
    }
}
