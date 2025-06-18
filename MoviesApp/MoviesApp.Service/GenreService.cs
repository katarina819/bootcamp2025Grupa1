using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MoviesApp.Model;
using MoviesAppRepository;
using Npgsql;

namespace MoviesAppService
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IList<object>> GetAllGenreAsync()

        {
            return await _genreRepository.GetAllGenreAsync();
        }

        public async Task DeleteGenreAsync(Guid id)
        {
            await _genreRepository.DeleteGenreAsync(id);

        }

        public async Task UpdateGenreAsync(Genre genre) 
        { 
            await _genreRepository.UpdateGenreAsync(genre);
        }

        public async Task AddGenreAsync(Genre genre)
        {
            await _genreRepository.AddGenreAsync(genre);
        }

        public async Task<Genre> GetGenreByIdAsync(Guid id)
        {
            return await _genreRepository.GetGenreByIdAsync(id);
        }

        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            return await _genreRepository.GetGenresByMovieIdAsync(movieId);
        }
    }

      
}

