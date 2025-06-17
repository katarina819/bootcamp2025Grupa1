using MoviesAppModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAppService
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreModels>> GetAllAsync(
            string? search = null,
            string? sort = null,
            int? page = null,
            int? pageSize = null);

        Task<GenreModels?> GetByIdAsync(Guid id);
        Task AddAsync(GenreModels genre);
        Task UpdateAsync(GenreModels genre);
        Task DeleteAsync(Guid id);
    }
}

