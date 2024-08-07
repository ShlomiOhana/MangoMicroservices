using AutoMapper;
using Mango.Services.AuthAPI.Models;
using Mango.Services.AuthAPI.Models.Dto;
namespace Mango.Services.AuthAPI.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UserDto>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
                .ReverseMap();
        }
    }
}
