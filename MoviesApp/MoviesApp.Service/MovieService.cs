using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Pagination;
using MoviesApp.Repository.Common;
using MoviesApp.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoviesApp.Service
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;
            _mapper = mapper;
        }

        public async Task<Paginated<MovieDto>> GetAllMoviesAsync(int page = 1, int pageSize = 10)
        {
            var pagedMovies = await _movieRepository.GetAllMoviesAsync(page, pageSize);
            return new Paginated<MovieDto>
            {
                Page = pagedMovies.Page,
                PageSize = pagedMovies.PageSize,
                TotalCount = pagedMovies.TotalCount,
                Items = _mapper.Map<List<MovieDto>>(pagedMovies.Items)
            };

        }

        public async Task DeleteMovieAsync(Guid id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if (movie == null)
            {
                throw new Exception();
            }
            await _movieRepository.DeleteMovieAsync(id);
        }
        public async Task UpdateMovieAsync(Movie movie)
        {
            await _movieRepository.UpdateMovieAsync(movie);
        }
        public async Task AddMovieAsync(Movie movie)
        {
            await _movieRepository.AddMovieAsync(movie);
        }

        public async Task<Movie> GetMovieByIdAsync(Guid id)
        {
            var movie = await _movieRepository.GetMovieByIdAsync(id);
            if(movie == null)
            {
                throw new Exception();
            }
            return movie;
        }

        public async Task<List<Movie>> GetMoviesSortedAsync(string sortBy, string order)
        {
            return await _movieRepository.GetMoviesSortedAsync(sortBy, order);
        }

        public async Task<List<Movie>> GetMoviesFilterNameAsync(string filter)
        {
            return await _movieRepository.GetMoviesFilterNameAsync(filter);
        }

        public async Task<IList<string>> GetGenresByMovieIdAsync(Guid movieId)
        {
            return await _movieRepository.GetGenresByMovieIdAsync(movieId);
        }

        public async Task<IList<string>> GetLanguagesByMovieIdAsync(Guid movieId)
        {
            return await _movieRepository.GetLanguagesByMovieIdAsync(movieId);
        }
        public async Task<DirectorCreateDto> GetDirectorByMovieIdAsync(Guid movieId)
        {
            return await _movieRepository.GetDirectorByMovieIdAsync(movieId);
        }
    }
}
