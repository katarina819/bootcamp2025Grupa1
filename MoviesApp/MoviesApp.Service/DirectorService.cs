using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Pagination;
using MoviesApp.Repository.Common;
using MoviesApp.Service.Common;

namespace MoviesApp.Service
{
    /// <summary>
    /// Service class for managing director-related operations.
    /// </summary>
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectorService"/> class.
        /// </summary>
        /// <param name="directorRepository">The director repository.</param>
        /// <param name="mapper">The AutoMapper instance.</param>
        public DirectorService(IDirectorRepository directorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves paginated list of directors.
        /// </summary>
        /// <param name="page">Page number (default is 1).</param>
        /// <param name="pageSize">Number of items per page (default is 10).</param>
        /// <returns>Paginated list of DirectorDto.</returns>
        public async Task<Paginated<DirectorDto>> GetAllDirectorsAsync(int page = 1, int pageSize = 10)
        {
            var pagedDirectors = await _directorRepository.GetAllDirectorsAsync(page, pageSize);

            return new Paginated<DirectorDto>
            {
                Page = pagedDirectors.Page,
                PageSize = pagedDirectors.PageSize,
                TotalCount = pagedDirectors.TotalCount,
                Items = _mapper.Map<List<DirectorDto>>(pagedDirectors.Items)
            };
        }

        /// <summary>
        /// Retrieves a director by its unique identifier.
        /// </summary>
        /// <param name="id">Director ID.</param>
        /// <returns>DirectorDto if found; otherwise, null.</returns>
        public async Task<DirectorDto?> GetDirectorByIdAsync(Guid id)
        {
            var director = await _directorRepository.GetDirectorByIdAsync(id);

            if (director == null)
                return null;
            return _mapper.Map<DirectorDto>(director);
        }

        /// <summary>
        /// Creates a new director.
        /// </summary>
        /// <param name="dto">DirectorCreateDto containing director data.</param>
        /// <returns>Created DirectorDto.</returns>
        public async Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto dto)
        {
            var director = _mapper.Map<Director>(dto);
            var createdDirector = await _directorRepository.CreateDirectorAsync(director);
            return _mapper.Map<DirectorDto>(createdDirector);
        }

        /// <summary>
        /// Updates an existing director identified by ID.
        /// </summary>
        /// <param name="id">Director ID.</param>
        /// <param name="dto">DirectorCreateDto containing updated data.</param>
        /// <returns>True if update was successful; otherwise, false.</returns>
        public async Task<bool> UpdateDirectorAsync(Guid id, DirectorCreateDto dto)
        {
            var existingDirector = await _directorRepository.GetDirectorByIdAsync(id);
            if (existingDirector == null)
                return false;

            _mapper.Map(dto, existingDirector);
            return await _directorRepository.UpdateDirectorAsync(existingDirector);
        }

        /// <summary>
        /// Deletes a director by ID.
        /// </summary>
        /// <param name="id">Director ID.</param>
        /// <returns>True if deletion was successful; otherwise, false.</returns>
        public async Task<bool> DeleteDirector(Guid id)
        {
            return await _directorRepository.DeleteDirectorAsync(id);
        }
    }
}
