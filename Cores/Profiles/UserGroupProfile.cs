using AutoMapper;
using Cores.Dtos.UserGroups;
using Cores.Dtos.UserInfo;
using Cores.Entities;

namespace Cores.Profiles
{
    public class UserGroupProfile : Profile
    {
        public UserGroupProfile()
        {
            CreateMap<UserGroup, UserGroupResponseDto>(); 
            CreateMap<UserGroupResponseDto, UserGroup>();
            CreateMap<UserGroupDto, UserGroup>();
        }
    }
}
