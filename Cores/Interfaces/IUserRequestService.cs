using Cores.Dtos.UserInfo;
using Cores.Dtos.UserRequests;
using Cores.Entities;

namespace Cores.Interfaces
{
    public interface IUserRequestService
    {
        Task<UserRequestResponseDto> CreateUserRequest(UserRequestCreateDto requestDto);
        Task DeleteUserRequest(long requestId);
        Task<UserRequestResponseDto> EditUserRequest(long requestId, UserRequestUpdateDto requestDto);
        Task<UserRequestResponseDto> GetUserRequestById(long requestId);
        Task<IEnumerable<UserRequestResponseDto>> GetAllUserRequests();
        Task<IEnumerable<UserRequestResponseDto>> GetUserRequestsByUser(string userId);
        Task<RequestSummaryDto> GetUserRequestsSummary(string userId);
    }
}
