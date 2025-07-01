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
        Task<Paginated<MovieDto>> GetAllMoviesAsync(int page = 1, int pageSize = 10);
        Task DeleteMovieAsync(Guid id);
        Task UpdateMovieAsync(Movie movie);
        Task AddMovieAsync(Movie movie);
        Task<Movie> GetMovieByIdAsync(Guid id);
        Task<Paginated<MovieDto>> GetMoviesSortedAsync(string sortBy, string order, int page = 1, int pageSize = 10);
        Task<Paginated<MovieDto>> GetMoviesFilterNameAsync(string filter, int page = 1, int pageSize = 10);
        Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId);
        Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId);
        Task<DirectorCreateDto> GetDirectorByMovieIdAsync(Guid movieId);
    }
}
