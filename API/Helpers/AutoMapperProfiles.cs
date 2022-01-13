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
            dest => dest.CongregationRoles, act => act.Ignore())
          .ForMember(
            dest => dest.Password, act => act.Ignore()
          );
        
        CreateMap<AddCongregationDto, Congregation>();
        CreateMap<CongregationRole, CongregationRoleDto>()
          .ForMember(
            dest => dest.Role, 
            opt => opt.MapFrom(src => src.Role.Name))
          .ForMember(
            dest => dest.Congregation,
            opt => opt.MapFrom(src => src.Congregation.Name));
        CreateMap<Congregation, CongregationDto>()
          .ForMember(
            dest => dest.Publishers,
            opt => opt.MapFrom(src => src.CongregationRoles
              .Select(cr => cr.UserId).Distinct().Count())
          );
          
    }

  }
}