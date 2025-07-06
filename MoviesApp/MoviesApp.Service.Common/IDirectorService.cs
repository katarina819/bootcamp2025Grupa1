using MoviesApp.Pagination;
using MoviesApp.DTO;

namespace MoviesApp.Service.Common
{
    /// <summary>
    /// Interface defining operations related to directors.
    /// </summary>
    public interface IDirectorService
    {
        /// <summary>
        /// Retrieves a paginated list of directors.
        /// </summary>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>Paginated list of DirectorDto objects.</returns>
        Task<Paginated<DirectorDto>> GetAllDirectorsAsync(int page = 1, int pageSize = 10);

        /// <summary>
        /// Retrieves a director by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the director.</param>
        /// <returns>A DirectorDto object if found; otherwise, null.</returns>
        Task<DirectorDto?> GetDirectorByIdAsync(Guid id);

        /// <summary>
        /// Creates a new director.
        /// </summary>
        /// <param name="dto">Data transfer object containing director creation details.</param>
        /// <returns>The created DirectorDto.</returns>
        Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto dto);

        /// <summary>
        /// Updates an existing director identified by id.
        /// </summary>
        /// <param name="id">The unique identifier of the director to update.</param>
        /// <param name="dto">Data transfer object containing updated director details.</param>
        /// <returns>True if the update was successful; otherwise, false.</returns>
        Task<bool> UpdateDirectorAsync(Guid id, DirectorCreateDto dto);

        /// <summary>
        /// Deletes a director by their unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the director to delete.</param>
        /// <returns>True if the deletion was successful; otherwise, false.</returns>
        Task<bool> DeleteDirector(Guid id);
    }
}
