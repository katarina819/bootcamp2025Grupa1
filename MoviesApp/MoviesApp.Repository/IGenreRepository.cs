using MoviesAppModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAppRepository
{
    public interface IGenreRepository
    {
        Task<IEnumerable<GenreModels>> GetAllAsync(
            string? search = null,
            string? sort = null,
            int? page = null,
            int? pageSize = null);

        Task<IEnumerable<GenreModels>> GetFilteredAsync(string? name, string? sort, int page, int pageSize);

        Task<GenreModels?> GetByIdAsync(Guid id);
        Task AddAsync(GenreModels genre);
        Task UpdateAsync(GenreModels genre);
        Task DeleteAsync(Guid id);
    }
}

