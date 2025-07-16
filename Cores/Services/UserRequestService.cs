using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using Cores.ApplicationDbContext;
using Cores.Dtos.UserRequests;
using Cores.Entities;
using Cores.Enums;
using Cores.Interfaces;

namespace Cores.Services
{
    public class UserRequestService : IUserRequestService
    {
        private readonly ApplicationDb _context;
        private readonly IMapper _mapper;

        public UserRequestService(ApplicationDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserRequestResponseDto> CreateUserRequest(UserRequestCreateDto requestDto)
        {
            var userRequest = _mapper.Map<UserRequest>(requestDto);
            userRequest.RequestDateTime = DateTime.UtcNow;
            userRequest.Status = (int)RequestStatus.Draft;

            _context.UserRequests.Add(userRequest);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserRequestResponseDto>(userRequest);
        }

        public async Task DeleteUserRequest(long requestId)
        {
            var request = await _context.UserRequests.FindAsync(requestId);
            _context.UserRequests.Remove(request);
            await _context.SaveChangesAsync();
        }

        public async Task<UserRequestResponseDto> EditUserRequest(long requestId, UserRequestUpdateDto requestDto)
        {
            var existingRequest = await _context.UserRequests.FindAsync(requestId);
            _mapper.Map(requestDto, existingRequest);

            if (requestDto.Status == (int)RequestStatus.Approved && existingRequest.CompletionDateTime == null)
            {
                existingRequest.CompletionDateTime = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<UserRequestResponseDto>(existingRequest);
        }

        public async Task<UserRequestResponseDto> GetUserRequestById(long requestId)
        {
            var request = await _context.UserRequests
                .Include(r => r.RequestedByUser)
                .FirstOrDefaultAsync(r => r.RequestId == requestId);
            return _mapper.Map<UserRequestResponseDto>(request);
        }

        public async Task<IEnumerable<UserRequestResponseDto>> GetAllUserRequests()
        {
            var requests = await _context.UserRequests
                .Include(r => r.RequestedByUser)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserRequestResponseDto>>(requests);
        }

        public async Task<IEnumerable<UserRequestResponseDto>> GetUserRequestsByUser(string userId)
        {
            var requests = await _context.UserRequests
                .Include(r => r.RequestedByUser)
                .Where(r => r.RequestedBy == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<UserRequestResponseDto>>(requests);
        }

        public async Task<RequestSummaryDto> GetUserRequestsSummary(string userId)
        {
            var userExists = await _context.UserInfos.AnyAsync(u => u.UserId == userId);
            var summary = await _context.UserRequests
                .Where(r => r.RequestedBy == userId)
                .GroupBy(r => r.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Count = g.Count()
                })
                .ToListAsync();

            var result = new RequestSummaryDto
            {
                UserId = userId,
                StatusCounts = summary.ToDictionary(
                    x => Enum.GetName(typeof(RequestStatus), x.Status) ?? $"Status {x.Status}",
                    x => x.Count)
            };

            return result;
        }
    }
}