using Cores.Dtos.UserGroups;

namespace Cores.Interfaces
{
    public interface IUserGroupService
    {
        Task<UserGroupResponseDto> CreateUserGroup(UserGroupCreateDto userGroupDto);
        Task DeleteUserGroup(string groupId);
        Task<UserGroupResponseDto> EditUserGroup(string groupId, UserGroupUpdateDto userGroupDto);
        Task<UserGroupResponseDto> AssignUsersToGroup(AssignUsersToGroupDto assignDto);
        Task<UserGroupResponseDto> AssignPoliciesToGroup(AssignPoliciesToGroupDto assignDto);
        Task<UserGroupResponseDto> GetUserGroupById(string groupId);
        Task<IEnumerable<UserGroupResponseDto>> GetAllUserGroups();
    }
}
