using AutoMapper;
using Cores.Dtos.UserInfo;
using Cores.Entities;

namespace Cores.Profiles
{
    public class UserInfoProfile : Profile
    {
        public UserInfoProfile()
        {
            CreateMap<UserInfo, UserInfoDto>()
                .ForMember(dest => dest.GroupsNames,
                          opt => opt.MapFrom(src => src.Groups.Select(g => g.Name)));
            CreateMap<UserInfoDto, UserInfo>();
            CreateMap<UserInfo, RegistrationResponseDto>();
        }
    }
}
