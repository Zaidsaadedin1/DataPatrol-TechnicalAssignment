using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserGroups
{
    public class AssignUsersToGroupDto
    {
        [Required]
        public string GroupId { get; set; } = null!;

        [Required]
        public IEnumerable<string> UserIds { get; set; } = new List<string>();
    }
}
