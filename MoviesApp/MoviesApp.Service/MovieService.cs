using MoviesAppModel;
using MoviesAppRepository;

namespace MoviesAppService
{
    public class MovieService
    {
        private readonly MovieRepository _movieRepository;

        public MovieService(MovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public Task<List<MovieModels>> GetAllMoviesAsync() => _movieRepository.GetAllAsync();

        public async Task<List<MovieWithGenreDto>> GetMoviesWithGenresAsync()
        {
            return await _movieRepository.GetMoviesWithGenresFlatAsync();
        }


        public Task<MovieModels?> GetMovieByIdAsync(Guid id) => _movieRepository.GetByIdAsync(id);

        public async Task AddMovieAsync(MovieModels movie)
        {
            if (movie.Id == Guid.Empty)
                movie.Id = Guid.NewGuid();

            await _movieRepository.AddAsync(movie);
        }

        public Task<bool> UpdateMovieAsync(MovieModels movie) => _movieRepository.UpdateAsync(movie);

        public Task<bool> DeleteMovieAsync(Guid id) => _movieRepository.DeleteAsync(id);
    }
}
