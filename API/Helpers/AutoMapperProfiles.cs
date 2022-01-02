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
        CreateMap<CongregationRole, CongregationRoleDto>()
          .ForMember(
            dest => dest.Role, 
            opt => opt.MapFrom(src => src.Role.Name))
          .ForMember(
            dest => dest.Congregation,
            opt => opt.MapFrom(src => src.Congregation.Name));

    }

  }
}