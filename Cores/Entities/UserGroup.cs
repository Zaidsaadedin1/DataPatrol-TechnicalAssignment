using System.ComponentModel.DataAnnotations;

namespace Cores.Entities
{
    public class UserGroup
    {
        [Key]
        [MaxLength(30)]
        public string GroupId { get; set; } = null!;

        [MaxLength(20)]
        public string Name { get; set; } = null!;

        [MaxLength(200)]
        public string Description { get; set; } = null!;

        public ICollection<UserInfo> Users { get; set; } = new List<UserInfo>();

        public ICollection<PolicyTable> Policies { get; set; } = new List<PolicyTable>();
    }
}
