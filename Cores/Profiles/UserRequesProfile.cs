using AutoMapper;
using Cores.Dtos.UserRequests;
using Cores.Entities;

namespace Cores.Mappings
{
    public class UserRequestProfile : Profile 
    {
        public UserRequestProfile()
        {
            CreateMap<UserRequest, UserRequestResponseDto>();
            CreateMap<UserRequestCreateDto, UserRequest>();
            CreateMap<UserRequestUpdateDto, UserRequest>();
        }
    }
}