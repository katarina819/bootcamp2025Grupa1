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
    /// <summary>
    /// Repository interface for managing movies and related data.
    /// </summary>
    public interface IMovieRepository
    {
        /// <summary>
        /// Retrieves paginated list of movies.
        /// </summary>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>Paginated list of MovieDto.</returns>
        Task<Paginated<MovieDto>> GetAllMoviesAsync(int page = 1, int pageSize = 10);

        /// <summary>
        /// Deletes a movie by its unique identifier.
        /// </summary>
        /// <param name="id">Movie ID.</param>
        Task DeleteMovieAsync(Guid id);

        /// <summary>
        /// Updates a movie using provided data transfer object.
        /// </summary>
        /// <param name="movie">MovieCreateDto containing updated data.</param>
        Task UpdateMovieAsync(MovieCreateDto movie);

        /// <summary>
        /// Adds a new movie to the repository.
        /// </summary>
        /// <param name="movie">MovieCreateDto containing movie data.</param>
        Task AddMovieAsync(MovieCreateDto movie);

        /// <summary>
        /// Retrieves a movie by its unique identifier.
        /// </summary>
        /// <param name="id">Movie ID.</param>
        /// <returns>Movie entity.</returns>
        Task<Movie> GetMovieByIdAsync(Guid id);

        /// <summary>
        /// Retrieves paginated and sorted movies with optional filtering by genre.
        /// </summary>
        /// <param name="sortBy">Column to sort by.</param>
        /// <param name="order">Sort order ("ASC" or "DESC").</param>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <param name="genreId">Optional genre ID to filter movies.</param>
        /// <returns>Paginated list of MovieDetailsDto.</returns>
        Task<Paginated<MovieDetailsDto>> GetMoviesSortedAsync(string sortBy, string order, int page = 1, int pageSize = 10, Guid? genreId = null);

        /// <summary>
        /// Retrieves paginated list of movies filtered by name.
        /// </summary>
        /// <param name="filter">Filter string for movie names.</param>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>Paginated list of MovieDto.</returns>
        Task<Paginated<MovieDto>> GetMoviesFilterNameAsync(string filter, int page = 1, int pageSize = 10);

        /// <summary>
        /// Retrieves a list of genre names associated with a given movie.
        /// </summary>
        /// <param name="movieId">Movie ID.</param>
        /// <returns>List of genre names.</returns>
        Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId);

        /// <summary>
        /// Retrieves a list of language names associated with a given movie.
        /// </summary>
        /// <param name="movieId">Movie ID.</param>
        /// <returns>List of language names.</returns>
        Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId);

        /// <summary>
        /// Retrieves director information by movie ID.
        /// </summary>
        /// <param name="movieId">Movie ID.</param>
        /// <returns>DirectorCreateDto with director details.</returns>
        Task<DirectorCreateDto> GetDirectorByIdAsync(Guid movieId);

        /// <summary>
        /// Retrieves a list of movie names filtered by a search string.
        /// </summary>
        /// <param name="filter">Filter string.</param>
        /// <returns>List of MovieNameDto.</returns>
        Task<List<MovieNameDto>> GetFilteredMovieNamesAsync(string filter);
    }
}
