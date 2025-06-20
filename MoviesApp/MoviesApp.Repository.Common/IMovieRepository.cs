using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Repository.Common
{
    public interface IMovieRepository
    {
        Task<Paginated<Movie>> GetAllMoviesAsync(int page = 1, int pageSize = 10);
        Task DeleteMovieAsync(Guid id);
        Task UpdateMovieAsync(Movie movie);
        Task AddMovieAsync(Movie movie);
        Task<Movie> GetMovieByIdAsync(Guid id);
        Task<List<Movie>> GetMoviesSortedAsync(string sortBy, string order);
        Task<List<Movie>> GetMoviesFilterNameAsync(string filter);
        Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId);
        Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId);
        Task<DirectorCreateDto> GetDirectorByMovieIdAsync(Guid movieId);
    }
}
