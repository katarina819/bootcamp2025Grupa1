using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoviesApp.Model;
using MoviesAppRepository;
using Npgsql;

namespace MoviesAppService
{
    /// <summary>
    /// Service class for managing genres.
    /// </summary>
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenreService"/> class.
        /// </summary>
        /// <param name="genreRepository">The genre repository.</param>
        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        /// <summary>
        /// Retrieves all genres.
        /// </summary>
        /// <returns>A list of all genres as objects.</returns>
        public async Task<IList<object>> GetAllGenreAsync()
        {
            return await _genreRepository.GetAllGenreAsync();
        }

        /// <summary>
        /// Deletes a genre by its unique identifier.
        /// </summary>
        /// <param name="id">The genre ID.</param>
        public async Task DeleteGenreAsync(Guid id)
        {
            await _genreRepository.DeleteGenreAsync(id);
        }

        /// <summary>
        /// Updates an existing genre.
        /// </summary>
        /// <param name="genre">The genre to update.</param>
        public async Task UpdateGenreAsync(Genre genre)
        {
            await _genreRepository.UpdateGenreAsync(genre);
        }

        /// <summary>
        /// Adds a new genre.
        /// </summary>
        /// <param name="genre">The genre to add.</param>
        public async Task AddGenreAsync(Genre genre)
        {
            await _genreRepository.AddGenreAsync(genre);
        }

        /// <summary>
        /// Retrieves a genre by its unique identifier.
        /// </summary>
        /// <param name="id">The genre ID.</param>
        /// <returns>The genre if found.</returns>
        public async Task<Genre> GetGenreByIdAsync(Guid id)
        {
            return await _genreRepository.GetGenreByIdAsync(id);
        }

        /// <summary>
        /// Retrieves a list of genre names associated with a specific movie.
        /// </summary>
        /// <param name="movieId">The movie ID.</param>
        /// <returns>A list of genre names.</returns>
        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            return await _genreRepository.GetGenresByMovieIdAsync(movieId);
        }
    }
}
