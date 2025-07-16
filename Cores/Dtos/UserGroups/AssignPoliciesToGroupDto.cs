using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserGroups
{
    public class AssignPoliciesToGroupDto
    {
        [Required]
        public string GroupId { get; set; } = null!;

        [Required]
        public IEnumerable<string> PolicyIds { get; set; } = new List<string>();
    }
}
