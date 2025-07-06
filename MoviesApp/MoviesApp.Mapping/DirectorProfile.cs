using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Model;

namespace MoviesApp.Mapping
{
    /// <summary>
    /// AutoMapper profile for mapping between Director entities and DTOs.
    /// </summary>
    public class DirectorProfile : Profile
    {
        /// <summary>
        /// Configures mappings between Director model and related DTOs.
        /// </summary>
        public DirectorProfile()
        {
            // Maps Director entity to DirectorDto and vice versa
            CreateMap<Director, DirectorDto>().ReverseMap();

            // Maps DirectorCreateDto to Director entity
            CreateMap<DirectorCreateDto, Director>();
        }
    }
}
