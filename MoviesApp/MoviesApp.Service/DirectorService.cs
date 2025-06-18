using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Model;
using MoviesApp.Pagination;
using MoviesApp.Repository;
using MoviesApp.Service.Common;

namespace MoviesApp.Service 
{
    public class DirectorService : IDirectorService
    {
        private readonly IDirectorRepository _directorRepository;
        private readonly IMapper _mapper;

        public DirectorService(IDirectorRepository directorRepository, IMapper mapper)
        {
            _directorRepository = directorRepository;
            _mapper = mapper;
        }

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
      

        public async Task<DirectorDto?> GetDirectorByIdAsync(Guid id)
        {
            var director = await _directorRepository.GetDirectorByIdAsync(id);
            
            if (director == null)
                return null;
            return _mapper.Map<DirectorDto>(director);
        }

        public async Task<DirectorDto> CreateDirectorAsync(DirectorCreateDto dto)
        {
            var director = _mapper.Map<Director>(dto);
            var createdDirector = await _directorRepository.CreateDirectorAsync(director);
            return _mapper.Map<DirectorDto>(createdDirector);
        }

        public async Task<bool> UpdateDirectorAsync(Guid id, DirectorCreateDto dto)
        {
            var existingDirector = await _directorRepository.GetDirectorByIdAsync(id);
            if (existingDirector == null)
                return false;

            _mapper.Map(dto, existingDirector);
            return await _directorRepository.UpdateDirectorAsync(existingDirector);
        }

        public async Task<bool> DeleteDirector(Guid id)
        {
            return await _directorRepository.DeleteDirectorAsync(id);
        }        
    }
}
