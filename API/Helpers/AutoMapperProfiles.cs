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
        
        CreateMap<AddCongregationDto, Congregation>();          
    }

  }
}