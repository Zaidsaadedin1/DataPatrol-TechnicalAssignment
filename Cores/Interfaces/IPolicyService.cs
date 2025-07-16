using Cores.Dtos.Policies;
using Cores.Entities;

namespace Cores.Interfaces
{
    public interface IPolicyService
    {
        Task<PolicyResponseDto> CreatePolicy(PolicyCreateDto policyDto);
        Task DeletePolicy(string policyId);
        Task<PolicyResponseDto> EditPolicy(string policyId, PolicyUpdateDto policyDto);
        Task<PolicyResponseDto> AssignPolicyToGroups(AssignPolicyToGroupsDto assignDto);
        Task<PolicyResponseDto> GetPolicyById(string policyId);
        Task<IEnumerable<PolicyResponseDto>> GetAllPolicies();
    }
}
