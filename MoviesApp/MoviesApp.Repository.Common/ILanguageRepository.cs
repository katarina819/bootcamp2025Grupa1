using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Model;

namespace MoviesApp.Repository.Common
{
    /// <summary>
    /// Interface for managing Language entities in the repository.
    /// </summary>
    public interface ILanguageRepository
    {
        /// <summary>
        /// Retrieves all languages.
        /// </summary>
        /// <returns>An enumerable collection of Language objects.</returns>
        Task<IEnumerable<Language>> GetAllAsync();

        /// <summary>
        /// Retrieves a language by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the language.</param>
        /// <returns>The Language object if found; otherwise, null.</returns>
        Task<Language?> GetByIdAsync(Guid id);

        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="language">The Language object to create.</param>
        Task CreateAsync(Language language);

        /// <summary>
        /// Updates an existing language.
        /// </summary>
        /// <param name="language">The Language object with updated data.</param>
        Task UpdateAsync(Language language);

        /// <summary>
        /// Deletes a language by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the language to delete.</param>
        Task DeleteAsync(Guid id);
    }
}
