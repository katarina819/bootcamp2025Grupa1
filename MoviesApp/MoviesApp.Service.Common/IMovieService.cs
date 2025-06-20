using MoviesApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Service.Common
{
    public interface IMovieService
    {
        Task<IList<object>> GetAllMoviesAsync();
        Task DeleteMovieAsync(Guid id);
        Task UpdateMovieAsync(Movie movie);
        Task AddMovieAsync(Movie movie);
        Task<Movie> GetMovieByIdAsync(Guid id);
        Task<List<Movie>> GetMoviesSortedAsync(string sortBy, string order);
        Task<List<Movie>> GetMoviesFilterNameAsync(string filter);
    }
}
