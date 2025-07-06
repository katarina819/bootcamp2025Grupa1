using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Model;
using MoviesApp.Repository.Common;
using MoviesApp.Service.Common;

namespace MoviesApp.Service
{
    /// <summary>
    /// Service class for managing languages.
    /// </summary>
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _repo;

        /// <summary>
        /// Initializes a new instance of the <see cref="LanguageService"/> class.
        /// </summary>
        /// <param name="repo">The language repository.</param>
        public LanguageService(ILanguageRepository repo) => _repo = repo;

        /// <summary>
        /// Retrieves all languages.
        /// </summary>
        /// <returns>A collection of all languages.</returns>
        public Task<IEnumerable<Language>> GetAllAsync() => _repo.GetAllAsync();

        /// <summary>
        /// Retrieves a language by its unique identifier.
        /// </summary>
        /// <param name="id">The language ID.</param>
        /// <returns>The language if found; otherwise, null.</returns>
        public Task<Language?> GetByIdAsync(Guid id) => _repo.GetByIdAsync(id);

        /// <summary>
        /// Creates a new language.
        /// </summary>
        /// <param name="language">The language to create.</param>
        public Task CreateAsync(Language language) => _repo.CreateAsync(language);

        /// <summary>
        /// Updates an existing language.
        /// </summary>
        /// <param name="language">The language to update.</param>
        public Task UpdateAsync(Language language) => _repo.UpdateAsync(language);

        /// <summary>
        /// Deletes a language by its unique identifier.
        /// </summary>
        /// <param name="id">The language ID.</param>
        public Task DeleteAsync(Guid id) => _repo.DeleteAsync(id);
    }
}
