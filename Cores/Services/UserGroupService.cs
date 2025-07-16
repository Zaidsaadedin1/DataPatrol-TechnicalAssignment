using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Cores.ApplicationDbContext;
using Cores.Dtos.UserGroups;
using Cores.Entities;
using Cores.Interfaces;

namespace Cores.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly ApplicationDb _context;
        private readonly IMapper _mapper;

        public UserGroupService(ApplicationDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserGroupResponseDto> CreateUserGroup(UserGroupCreateDto userGroupDto)
        {
            var userGroup = _mapper.Map<UserGroup>(userGroupDto);
            _context.UserGroups.Add(userGroup);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserGroupResponseDto>(userGroup);
        }

        public async Task DeleteUserGroup(string groupId)
        {
            var userGroup = await _context.UserGroups.FindAsync(groupId);

            _context.UserGroups.Remove(userGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<UserGroupResponseDto> EditUserGroup(string groupId, UserGroupUpdateDto userGroupDto)
        {
            var existingGroup = await _context.UserGroups.FindAsync(groupId);

            _mapper.Map(userGroupDto, existingGroup);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserGroupResponseDto>(existingGroup);
        }

        public async Task<UserGroupResponseDto> AssignUsersToGroup(AssignUsersToGroupDto assignDto)
        {
            var group = await _context.UserGroups
                .Include(g => g.Users)
                .FirstOrDefaultAsync(g => g.GroupId == assignDto.GroupId);


            var users = await _context.UserInfos
                .Where(u => assignDto.UserIds.Contains(u.UserId))
                .ToListAsync();

            group.Users = users;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserGroupResponseDto>(group);
        }

        public async Task<UserGroupResponseDto> AssignPoliciesToGroup(AssignPoliciesToGroupDto assignDto)
        {
            var group = await _context.UserGroups
                .Include(g => g.Policies)
                .FirstOrDefaultAsync(g => g.GroupId == assignDto.GroupId);

            var policies = await _context.Policies
                .Where(p => assignDto.PolicyIds.Contains(p.PolicyId))
                .ToListAsync();

            group.Policies = policies;
            await _context.SaveChangesAsync();
            return _mapper.Map<UserGroupResponseDto>(group);
        }

        public async Task<UserGroupResponseDto> GetUserGroupById(string groupId)
        {
            var group = await _context.UserGroups
                .Include(g => g.Users)
                .Include(g => g.Policies)
                .FirstOrDefaultAsync(g => g.GroupId == groupId);


            return _mapper.Map<UserGroupResponseDto>(group);
        }

        public async Task<IEnumerable<UserGroupResponseDto>> GetAllUserGroups()
        {
            var groups = await _context.UserGroups
                .Include(g => g.Users)
                .Include(g => g.Policies)
                .ToListAsync();

            return _mapper.Map<IEnumerable<UserGroupResponseDto>>(groups);
        }
    }
}


