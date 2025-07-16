using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserGroups
{
    public class UserGroupUpdateDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = null!;
    }
}
