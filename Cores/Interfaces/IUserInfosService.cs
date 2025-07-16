using Cores.Dtos.UserInfo;
using Cores.Dtos.UserRequests;
using Cores.Entities;

namespace Cores.Interfaces
{
    public interface IUserInfosService
    {
        Task<List<UserInfoDto>> GetAllUsersAsync();
        Task DeleteUserAsync(string userId);
        Task DisableUserAsync(string userId);
        Task AssignGroupsAsync(string userId, List<string> groupIds);
        Task<int> GetUserRequestsAsync(string userId);
        Task<List<UserRequestDto>> GetUserRequestsDetailedAsync(string userId);
        Task<List<UserGroupDto>> GetAllGroupsAsync();
        Task<RegistrationResponseDto> RegisterUserAsync(string username);
    }
}
