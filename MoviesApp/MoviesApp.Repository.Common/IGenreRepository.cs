using MoviesApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAppRepository
{
    public interface IGenreRepository
    {
        Task<IEnumerable<Genre>> GetAllAsync(
            string? search = null,
            string? sort = null,
            int? page = null,
            int? pageSize = null);

        Task<IEnumerable<Genre>> GetFilteredAsync(string? name, string? sort, int page, int pageSize);

        Task<Genre?> GetByIdAsync(Guid id);
        Task AddAsync(Genre genre);
        Task UpdateAsync(Genre genre);
        Task DeleteAsync(Guid id);
    }
}

