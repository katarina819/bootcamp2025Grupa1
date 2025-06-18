using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using MoviesApp.Model;

namespace MoviesAppRepository
{
    public interface IGenreRepository
    {
        Task<IList<object>> GetAllGenreAsync();
        Task DeleteGenreAsync(Guid id);
        Task UpdateGenreAsync(Genre genre);
        Task AddGenreAsync(Genre genre);
        Task<Genre> GetGenreByIdAsync(Guid id);






    }
}

