using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Pagination;
using MoviesApp.Repository.Common;
using MoviesApp.Service.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Service
{
    /// <summary>
    /// Service class responsible for movie-related operations.
    /// </summary>
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovieService"/> class.
        /// </summary>
        /// <param name="movieRepository">The movie repository.</param>
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
             _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Retrieves paginated list of all movies.
        /// </summary>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>Paginated list of movies.</returns>
        public async Task<Paginated<MovieDto>> GetAllMoviesAsync(int page = 1, int pageSize = 10)
        {
            return await _movieRepository.GetAllMoviesAsync(page, pageSize);
        }

        /// <summary>
        /// Deletes a movie by its unique identifier.
        /// Throws exception if movie does not exist.
        /// </summary>
        /// <param name="id">Movie ID.</param>
        public async Task DeleteMovieAsync(Guid id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                throw new Exception();
            }
            await _movieRepository.DeleteMovieAsync(id);
        }

        /// <summary>
        /// Updates an existing movie.
        /// </summary>
        /// <param name="movie">Movie data transfer object for update.</param>
        public async Task UpdateMovieAsync(MovieCreateDto movie)
        {
            await _movieRepository.UpdateMovieAsync(movie);
        }

        /// <summary>
        /// Adds a new movie.
        /// </summary>
        /// <param name="movie">Movie data transfer object to add.</param>
        public async Task AddMovieAsync(MovieCreateDto movie)
        {
            await _movieRepository.AddMovieAsync(movie);
        }

        /// <summary>
        /// Retrieves a movie by its unique identifier.
        /// Throws exception if movie does not exist.
        /// </summary>
        /// <param name="id">Movie ID.</param>
        /// <returns>The movie entity.</returns>
        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                throw new Exception();
            }
            return movie;
        }

        /// <summary>
        /// Retrieves paginated and sorted movies with optional genre filtering.
        /// </summary>
        /// <param name="sortBy">Field to sort by.</param>
        /// <param name="order">Sort order (asc/desc).</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Items per page.</param>
        /// <param name="genreId">Optional genre ID to filter by.</param>
        /// <returns>Paginated list of movie details.</returns>
        public async Task<Paginated<MovieDetailsDto>> GetMoviesSortedAsync(string sortBy, string order, int page = 1, int pageSize = 10, Guid? genreId = null)
        {
            return await _movieRepository.GetMoviesSortedAsync(sortBy, order, page, pageSize, genreId);
        }

        /// <summary>
        /// Retrieves paginated list of movies filtered by name.
        /// </summary>
        /// <param name="filter">Name filter string.</param>
        /// <param name="page">Page number.</param>
        /// <param name="pageSize">Items per page.</param>
        /// <returns>Paginated list of movies matching filter.</returns>
        public async Task<Paginated<MovieDto>> GetMoviesFilterNameAsync(string filter, int page = 1, int pageSize = 10)
        {
            return await _movieRepository.GetMoviesFilterNameAsync(filter, page, pageSize);
        }

        /// <summary>
        /// Retrieves the list of genres associated with a movie.
        /// </summary>
        /// <param name="movieId">Movie ID.</param>
        /// <returns>List of genre names.</returns>
        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            return await _movieRepository.GetGenresByMovieIdAsync(movieId);
        }

        /// <summary>
        /// Retrieves the list of languages associated with a movie.
        /// </summary>
        /// <param name="movieId">Movie ID.</param>
        /// <returns>List of language names.</returns>
        public async Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId)
        {
            return await _movieRepository.GetLanguagesByMovieIdAsync(movieId);
        }

        /// <summary>
        /// Retrieves the director details for a movie.
        /// </summary>
        /// <param name="movieId">Movie ID.</param>
        /// <returns>Director data transfer object.</returns>
        public async Task<DirectorCreateDto> GetDirectorByIdAsync(Guid movieId)
        {
            return await _movieRepository.GetDirectorByIdAsync(movieId);
        }

        /// <summary>
        /// Retrieves a filtered list of movie names matching a filter string.
        /// </summary>
        /// <param name="filter">Filter string for movie names.</param>
        /// <returns>List of movie name DTOs.</returns>
        public async Task<List<MovieNameDto>> GetFilteredMovieNamesAsync(string filter)
        {
            return await _movieRepository.GetFilteredMovieNamesAsync(filter);
        }
    }
}
