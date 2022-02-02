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
        CreateMap<AppUser, UserInfoDto>()
          .ForMember(
            dest => dest.FirstName, opt => opt.MapFrom(
              src => src.AssignedPublishers.FirstOrDefault().Publisher.FirstName)
          )
          .ForMember(
            dest => dest.Surname, opt => opt.MapFrom(
              src => src.AssignedPublishers.FirstOrDefault().Publisher.Surname)
          )
          .ForMember(
            dest => dest.Congregation, opt => opt.MapFrom(
              src => src.AssignedPublishers.FirstOrDefault().Publisher.Congregation.Name)
          );
        CreateMap<AppUser, UserToUpdateDto>();
        CreateMap<AppUser, UserTokenDto>();
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
        CreateMap<PublisherDto, Publisher>()
          .ForMember(dest => dest.Congregation, opt => opt.Ignore())
          .ForMember(dest => dest.Role, opt => opt.Ignore());
        CreateMap<AddCongregationDto, Congregation>();          
        CreateMap<Congregation, CongregationDto>()
          .ForMember(
            dest => dest.Publishers, opt => opt.MapFrom(
              src => src.Publishers.Count()
            )
          );  
        CreateMap<PublisherUpdateDto, Publisher>();  
        CreateMap<PublisherDto, PublisherSearchDto>();     
    }

  }
}