using MoviesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Repository.Common
{
    public interface IMovieRepository
    {
        Task<IList<object>> GetAllMoviesAsync();
        Task DeleteMovieAsync(Guid id);
        Task UpdateMovieAsync(Movie movie);
        Task AddMovieAsync(Movie movie);
        Task<Movie> GetMovieByIdAsync(Guid id);
    }
}
