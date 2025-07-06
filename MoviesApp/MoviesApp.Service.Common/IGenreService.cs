using MoviesApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAppService
{
    /// <summary>
    /// Service interface for handling genre-related operations.
    /// </summary>
    public interface IGenreService
    {
        /// <summary>
        /// Retrieves all genres.
        /// </summary>
        /// <returns>A list of all genres as objects.</returns>
        Task<IList<object>> GetAllGenreAsync();

        /// <summary>
        /// Deletes a genre by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the genre to delete.</param>
        Task DeleteGenreAsync(Guid id);

        /// <summary>
        /// Updates an existing genre.
        /// </summary>
        /// <param name="genre">The genre entity with updated information.</param>
        Task UpdateGenreAsync(Genre genre);

        /// <summary>
        /// Adds a new genre.
        /// </summary>
        /// <param name="genre">The genre entity to add.</param>
        Task AddGenreAsync(Genre genre);

        /// <summary>
        /// Retrieves a genre by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the genre.</param>
        /// <returns>The genre entity matching the specified id.</returns>
        Task<Genre> GetGenreByIdAsync(Guid id);

        /// <summary>
        /// Retrieves genres associated with a specific movie.
        /// </summary>
        /// <param name="movieId">The unique identifier of the movie.</param>
        /// <returns>A list of genre names linked to the movie.</returns>
        Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId);
    }
}
