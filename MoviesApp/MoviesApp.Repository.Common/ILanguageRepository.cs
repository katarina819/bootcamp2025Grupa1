using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Model;

namespace MoviesApp.Repository.Common
{
    public interface ILanguageRepository
    {
        Task<IEnumerable<Language>> GetAllAsync();
        Task<Language?> GetByIdAsync(Guid id);
        Task CreateAsync(Language language);
        Task UpdateAsync(Language language);
        Task DeleteAsync(Guid id);
    }
}
