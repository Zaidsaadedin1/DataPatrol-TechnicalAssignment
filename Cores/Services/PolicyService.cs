using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Cores.ApplicationDbContext;
using Cores.Dtos.Policies;
using Cores.Entities;
using Cores.Interfaces;

namespace Cores.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly ApplicationDb _context;
        private readonly IMapper _mapper;

        public PolicyService(ApplicationDb context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PolicyResponseDto> CreatePolicy(PolicyCreateDto policyDto)
        {
            var policy = _mapper.Map<PolicyTable>(policyDto);
            policy.CreatedDateTime = DateTime.UtcNow;
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
            return _mapper.Map<PolicyResponseDto>(policy);
        }

        public async Task DeletePolicy(string policyId)
        {
            var policy = await _context.Policies.FindAsync(policyId);


            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
        }

        public async Task<PolicyResponseDto> EditPolicy(string policyId, PolicyUpdateDto policyDto)
        {
            var existingPolicy = await _context.Policies.FindAsync(policyId);


            _mapper.Map(policyDto, existingPolicy);
            await _context.SaveChangesAsync();
            return _mapper.Map<PolicyResponseDto>(existingPolicy);
        }

        public async Task<PolicyResponseDto> AssignPolicyToGroups(AssignPolicyToGroupsDto assignDto)
        {
            var policy = await _context.Policies
                .Include(p => p.Groups)
                .FirstOrDefaultAsync(p => p.PolicyId == assignDto.PolicyId);

  

            var groups = await _context.UserGroups
                .Where(g => assignDto.GroupIds.Contains(g.GroupId))
                .ToListAsync();

            policy.Groups = groups;
            await _context.SaveChangesAsync();
            return _mapper.Map<PolicyResponseDto>(policy);
        }

        public async Task<PolicyResponseDto> GetPolicyById(string policyId)
        {
            var policy = await _context.Policies
                .Include(p => p.Groups)
                .FirstOrDefaultAsync(p => p.PolicyId == policyId);


            return _mapper.Map<PolicyResponseDto>(policy);
        }

        public async Task<IEnumerable<PolicyResponseDto>> GetAllPolicies()
        {
            var policies = await _context.Policies
                .Include(p => p.Groups)
                .ToListAsync();

            return _mapper.Map<IEnumerable<PolicyResponseDto>>(policies);
        }
    }
}
