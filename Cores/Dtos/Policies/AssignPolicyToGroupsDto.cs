using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.Policies
{
    public class AssignPolicyToGroupsDto
    {
        [Required]
        public string PolicyId { get; set; } = null!;

        [Required]
        public IEnumerable<string> GroupIds { get; set; } = new List<string>();
    }
}
