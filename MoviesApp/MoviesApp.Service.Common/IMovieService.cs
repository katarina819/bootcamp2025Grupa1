using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Service.Common
{
    /// <summary>
    /// Service interface for managing movies and related operations.
    /// </summary>
    public interface IMovieService
    {
        /// <summary>
        /// Retrieves a paginated list of all movies.
        /// </summary>
        /// <param name="page">The page number to retrieve (default is 1).</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        /// <returns>A paginated list of MovieDto.</returns>
        Task<Paginated<MovieDto>> GetAllMoviesAsync(int page = 1, int pageSize = 10);

        /// <summary>
        /// Deletes a movie by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie to delete.</param>
        Task DeleteMovieAsync(Guid id);

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        /// <param name="movie">The MovieCreateDto containing updated movie data.</param>
        Task UpdateMovieAsync(MovieCreateDto movie);

        /// <summary>
        /// Adds a new movie.
        /// </summary>
        /// <param name="movie">The MovieCreateDto representing the new movie.</param>
        Task AddMovieAsync(MovieCreateDto movie);

        /// <summary>
        /// Retrieves a movie by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the movie.</param>
        /// <returns>The Movie entity if found.</returns>
        Task<Movie> GetMovieByIdAsync(Guid id);

        /// <summary>
        /// Retrieves a paginated list of movies sorted by a specified field and order, optionally filtered by genre.
        /// </summary>
        /// <param name="sortBy">The field to sort by.</param>
        /// <param name="order">The sort order ("asc" or "desc").</param>
        /// <param name="page">The page number to retrieve (default is 1).</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        /// <param name="genreId">Optional genre identifier to filter movies.</param>
        /// <returns>A paginated list of MovieDetailsDto.</returns>
        Task<Paginated<MovieDetailsDto>> GetMoviesSortedAsync(string sortBy, string order, int page = 1, int pageSize = 10, Guid? genreId = null);

        /// <summary>
        /// Retrieves a paginated list of movies filtered by their name.
        /// </summary>
        /// <param name="filter">The filter string to apply on movie names.</param>
        /// <param name="page">The page number to retrieve (default is 1).</param>
        /// <param name="pageSize">The number of items per page (default is 10).</param>
        /// <returns>A paginated list of MovieDto.</returns>
        Task<Paginated<MovieDto>> GetMoviesFilterNameAsync(string filter, int page = 1, int pageSize = 10);

        /// <summary>
        /// Retrieves a list of genres associated with a specific movie.
        /// </summary>
        /// <param name="movieId">The unique identifier of the movie.</param>
        /// <returns>A list of genre names.</returns>
        Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId);

        /// <summary>
        /// Retrieves a list of languages associated with a specific movie.
        /// </summary>
        /// <param name="movieId">The unique identifier of the movie.</param>
        /// <returns>A list of language names.</returns>
        Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId);

        /// <summary>
        /// Retrieves the director information associated with a specific movie.
        /// </summary>
        /// <param name="movieId">The unique identifier of the movie.</param>
        /// <returns>A DirectorCreateDto representing the director.</returns>
        Task<DirectorCreateDto> GetDirectorByIdAsync(Guid movieId);

        /// <summary>
        /// Retrieves a list of movie names filtered by the given string.
        /// </summary>
        /// <param name="filter">The filter string to apply on movie names.</param>
        /// <returns>A list of filtered movie names as MovieNameDto.</returns>
        Task<List<MovieNameDto>> GetFilteredMovieNamesAsync(string filter);
    }
}
