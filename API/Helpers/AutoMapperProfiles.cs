using System.Linq;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    public AutoMapperProfiles()
    {
        CreateMap<AppUser, UserInfoDto>();
        CreateMap<AppUser, UserToUpdateDto>();
        CreateMap<AppUser, UserDto>();
        CreateMap<RegisterDto, AppUser>()
          .ForMember(
            dest => dest.PasswordHash, act => act.Ignore()
          );
        CreateMap<Publisher, PublisherDto>()
          .ForMember(
            dest => dest.Congregation, opt => opt.MapFrom(
              src => src.Congregation.Name)
          )
          .ForMember(
            dest => dest.Role, opt => opt.MapFrom(
              src => src.Role.Name
            )
          );

        CreateMap<AddCongregationDto, Congregation>();          
    }

  }
}