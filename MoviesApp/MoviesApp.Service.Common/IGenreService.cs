using MoviesApp.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoviesAppService
{
    public interface IGenreService
    {
        Task<IList<object>> GetAllGenreAsync();
        Task DeleteGenreAsync(Guid id);
        Task UpdateGenreAsync(Genre genre);
        Task AddGenreAsync(Genre genre);
        Task<Genre> GetGenreByIdAsync(Guid id);
    }
}

