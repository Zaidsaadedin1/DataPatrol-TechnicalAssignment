using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Cores.ApplicationDbContext;
using Cores.Dtos.UserInfo;
using Cores.Entities;
using Cores.Interfaces;

namespace Cores.Services
{
    public class UserInfosService : IUserInfosService
    {
        private readonly ApplicationDb _context;
        private readonly IMapper _mapper;

        public UserInfosService(ApplicationDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserInfoDto>> GetAllUsersAsync()
        {
            var users = await _context.UserInfos
                .Include(u => u.Groups)
                .Where(u => u.IsEnabled) 
                .ToListAsync();

            return _mapper.Map<List<UserInfoDto>>(users);
        }

        public async Task DisableUserAsync(string userId)
        {
            var user = await _context.UserInfos.FindAsync(userId);

            user.IsEnabled = false;
            await _context.SaveChangesAsync();
        }

        public async Task AssignGroupsAsync(string userId, List<string> groupIds)
        {
            var user = await _context.UserInfos
                .Include(u => u.Groups)
                .FirstOrDefaultAsync(u => u.UserId == userId);

            var groups = await _context.UserGroups
                .Where(g => groupIds.Contains(g.GroupId))
                .ToListAsync();

            user.Groups = groups;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetUserRequestsAsync(string userId)
        {
            var userExists = await _context.UserInfos.AnyAsync(u => u.UserId == userId);
   

            return await _context.UserRequests
                .CountAsync(r => r.RequestedBy == userId);
        }

        public async Task<List<UserRequestDto>> GetUserRequestsDetailedAsync(string userId)
        {
            var userExists = await _context.UserInfos.AnyAsync(u => u.UserId == userId);
   

            var requests = await _context.UserRequests
                .Where(r => r.RequestedBy == userId)
                .OrderByDescending(r => r.RequestDateTime)
                .ToListAsync();

            return _mapper.Map<List<UserRequestDto>>(requests);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _context.UserInfos.FindAsync(userId);

            var hasPendingRequests = await _context.UserRequests
                .AnyAsync(r => r.RequestedBy == userId && r.Status != 2);
            _context.UserInfos.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserGroupDto>> GetAllGroupsAsync()
        {
            var groups = await _context.UserGroups.ToListAsync();
            return _mapper.Map<List<UserGroupDto>>(groups);
        }

        public async Task<RegistrationResponseDto> RegisterUserAsync(string username)
        {
            var baseUserId = new string(username.ToLower()
                .Where(c => char.IsLetterOrDigit(c) || c == '_')
                .ToArray());

            if (string.IsNullOrEmpty(baseUserId))
            {
                baseUserId = "user";
            }

            var existingIds = await _context.UserInfos
                .Where(u => u.UserId.StartsWith(baseUserId))
                .Select(u => u.UserId)
                .ToListAsync();

            string userId;
            if (!existingIds.Contains(baseUserId))
            {
                userId = baseUserId;
            }
            else
            {
                int suffix = 1;
                do
                {
                    userId = $"{baseUserId}_{suffix.ToString().PadLeft(2, '0')}";
                    suffix++;
                } while (existingIds.Contains(userId));
            }

            var newUser = new UserInfo
            {
                UserId = userId,
                Username = username,
                IsEnabled = true,
                CreatedDateTime = DateTime.UtcNow
            };

            _context.UserInfos.Add(newUser);
            await _context.SaveChangesAsync();

            return new RegistrationResponseDto
            {
                UserId = userId,
                IsEnabled = newUser.IsEnabled
            };
        }
    }
}