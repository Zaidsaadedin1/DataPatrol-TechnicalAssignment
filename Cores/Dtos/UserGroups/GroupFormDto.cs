using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserGroups
{
    public class GroupFormDto
    {
        [Required(ErrorMessage = "Group name is required")]
        [MaxLength(20, ErrorMessage = "Group name cannot exceed 20 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; } = string.Empty;
    }
}
