using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MoviesApp.Model;
using MoviesApp.Repository.Common;
using MoviesApp.Service.Common;

namespace MoviesApp.Service
{
    public class LanguageService : ILanguageService
    {
        private readonly ILanguageRepository _repo;
        public LanguageService(ILanguageRepository repo) => _repo = repo;

        public Task<IEnumerable<Language>> GetAllAsync()          => _repo.GetAllAsync();
        public Task<Language?> GetByIdAsync(Guid id)              => _repo.GetByIdAsync(id);
        public Task CreateAsync(Language language)                => _repo.CreateAsync(language);
        public Task UpdateAsync(Language language)                => _repo.UpdateAsync(language);
        public Task DeleteAsync(Guid id)                          => _repo.DeleteAsync(id);
    }
}
