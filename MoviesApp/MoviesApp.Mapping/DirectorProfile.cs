using AutoMapper;
using MoviesApp.DTO;
using MoviesApp.Model;

namespace MoviesApp.Mapping
{
    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<Director, DirectorDto>().ReverseMap();
            CreateMap<DirectorCreateDto, Director>();
        }
    }
}
