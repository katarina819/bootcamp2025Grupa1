using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Model;

namespace MoviesApp.Service.Common
{
    /// <summary>
    /// Service interface for managing languages.
    /// </summary>
    public interface ILanguageService
    {
        /// <summary>
        /// Retrieves all languages.
        /// </summary>
        /// <returns>A collection of all Language entities.</returns>
        Task<IEnumerable<Language>> GetAllAsync();

        /// <summary>
        /// Retrieves a language by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the language.</param>
        /// <returns>The Language entity if found; otherwise, null.</returns>
        Task<Language?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="language">The Language entity to create.</param>
        Task CreateAsync(Language language);

        /// <summary>
        /// Updates an existing language.
        /// </summary>
        /// <param name="language">The Language entity with updated data.</param>
        Task UpdateAsync(Language language);

        /// <summary>
        /// Deletes a language by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the language to delete.</param>
        Task DeleteAsync(Guid id);
    }
}
