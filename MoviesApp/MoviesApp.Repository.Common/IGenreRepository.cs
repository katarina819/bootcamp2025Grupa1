using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MoviesApp.Model;

namespace MoviesAppRepository
{
    /// <summary>
    /// Interface for managing Genre entities in the repository.
    /// </summary>
    public interface IGenreRepository
    {
        /// <summary>
        /// Retrieves all genres.
        /// </summary>
        /// <returns>A list of genre objects.</returns>
        Task<IList<object>> GetAllGenreAsync();

        /// <summary>
        /// Deletes a genre by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to delete.</param>
        Task DeleteGenreAsync(Guid id);

        /// <summary>
        /// Updates an existing genre.
        /// </summary>
        /// <param name="genre">The genre object with updated data.</param>
        Task UpdateGenreAsync(Genre genre);

        /// <summary>
        /// Adds a new genre.
        /// </summary>
        /// <param name="genre">The genre object to add.</param>
        Task AddGenreAsync(Genre genre);

        /// <summary>
        /// Retrieves a genre by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the genre.</param>
        /// <returns>The genre object.</returns>
        Task<Genre> GetGenreByIdAsync(Guid id);

        /// <summary>
        /// Retrieves a list of genre names associated with a specific movie.
        /// </summary>
        /// <param name="movieId">The unique identifier of the movie.</param>
        /// <returns>A list of genre names.</returns>
        Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId);
    }
}
