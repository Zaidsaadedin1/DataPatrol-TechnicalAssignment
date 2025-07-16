using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserInfo
{
    public class AssignGroupsDto
    {
        [Required]
        public string UserId { get; set; } = null!;

        [Required]
        public List<string> GroupIds { get; set; } = new();
    }
}
