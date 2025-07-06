using MoviesApp.Pagination;
using MoviesApp.Model;

namespace MoviesApp.Repository.Common
{
    /// <summary>
    /// Interface for managing Director entities in the repository.
    /// </summary>
    public interface IDirectorRepository
    {
        /// <summary>
        /// Retrieves a paginated list of all directors.
        /// </summary>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>Paginated list of Director objects.</returns>
        Task<Paginated<Director>> GetAllDirectorsAsync(int page = 1, int pageSize = 10);

        /// <summary>
        /// Retrieves a director by their unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the director.</param>
        /// <returns>Director object if found; otherwise, null.</returns>
        Task<Director?> GetDirectorByIdAsync(Guid id);

        /// <summary>
        /// Creates a new director entity.
        /// </summary>
        /// <param name="director">Director object to create.</param>
        /// <returns>The created Director object.</returns>
        Task<Director> CreateDirectorAsync(Director director);

        /// <summary>
        /// Updates an existing director entity.
        /// </summary>
        /// <param name="director">Director object with updated data.</param>
        /// <returns>True if update was successful; otherwise, false.</returns>
        Task<bool> UpdateDirectorAsync(Director director);

        /// <summary>
        /// Deletes a director by their unique identifier.
        /// </summary>
        /// <param name="id">Unique identifier of the director to delete.</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteDirectorAsync(Guid id);
    }
}
