using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using MoviesAppModel;
using MoviesAppRepository;
using Microsoft.Extensions.Logging;

namespace MoviesAppService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _repository;
        private readonly ILogger<GenreService> _logger;

        public GenreService(IGenreRepository repository, ILogger<GenreService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public Task<IEnumerable<GenreModels>> GetAllAsync(string? name, string? sort, int? page, int? pageSize)
        {
            return _repository.GetAllAsync(name, sort, page, pageSize);
        }
        public Task<GenreModels?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public async Task AddAsync(GenreModels genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name))
                throw new ArgumentException("Genre name cannot be empty");

            var existingGenres = await _repository.GetAllAsync(genre.Name, null, 1, 1);
            if (existingGenres.Any(g => g.Name.Equals(genre.Name, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException("Genre with this name already exists.");

            await _repository.AddAsync(genre);
        }

        public async Task UpdateAsync(GenreModels genre)
        {
            if (string.IsNullOrWhiteSpace(genre.Name))
                throw new ArgumentException("Genre name cannot be empty");

            var existing = await _repository.GetByIdAsync(genre.Id);
            if (existing == null)
                throw new KeyNotFoundException("Genre not found");

            await _repository.UpdateAsync(genre);
        }
        public async Task DeleteAsync(Guid id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException("Genre not found");

            await _repository.DeleteAsync(id);
        }


        public async Task<IEnumerable<GenreModels>> GetFilteredAsync(string? name, string? sort, int page, int pageSize)
        {
            _logger.LogInformation("Fetching filtered genres. Name filter: {Name}, Sort: {Sort}, Page: {Page}, PageSize: {PageSize}",
                name, sort, page, pageSize);

            var result = await _repository.GetFilteredAsync(name, sort, page, pageSize);

            _logger.LogInformation("Fetched {Count} genres", result.Count());

            return result;
        }
    }
}

