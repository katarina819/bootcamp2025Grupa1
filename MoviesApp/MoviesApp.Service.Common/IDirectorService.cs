using MoviesApp.Pagination;
using MoviesApp.DTO;

namespace MoviesApp.Service.Common
{
    public interface IDirectorService
    {
        Task<Paginated<DirectorDto>> GetAllDirectorsAsync(int page = 1, int pageSize = 10);
        Task<DirectorDto?> GetDirectorByIdAsync(Guid id);
        Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto dto);
        Task<bool> UpdateDirectorAsync(Guid id, DirectorCreateDto dto);
        Task<bool> DeleteDirector(Guid id);
    }
}
