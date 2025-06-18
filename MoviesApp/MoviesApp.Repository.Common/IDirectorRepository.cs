using MoviesApp.Pagination;
using MoviesApp.Model;


namespace MoviesApp.Repository
{
    public interface IDirectorRepository
    {
        Task<Paginated<Director>> GetAllDirectorsAsync(int page = 1, int pageSize = 10);
        Task<Director?> GetDirectorByIdAsync(Guid id);
        Task<Director> CreateDirectorAsync(Director director);
        Task<bool> UpdateDirectorAsync(Director director);
        Task<bool> DeleteDirectorAsync(Guid id);
    }
}
