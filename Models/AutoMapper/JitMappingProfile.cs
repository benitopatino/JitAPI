using AutoMapper;
using JitAPI.Models.DTOS;

namespace JitAPI.Models.AutoMapper
{
    public class JitMappingProfile : Profile
    {
        public JitMappingProfile()
        {
            CreateMap<Jit, JitPostDTO>().ReverseMap();
            CreateMap<Jit, JitGetDTO>().ReverseMap();
            CreateMap<Jit, JitPutDTO>().ReverseMap();
            CreateMap<User, UserGetDTO>().ReverseMap();
        }
    }
}
