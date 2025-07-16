using System.ComponentModel.DataAnnotations;

namespace Cores.Entities
{
    public class PolicyTable
    {
        [Key]
        [MaxLength(30)]
        public string PolicyId { get; set; } = null!;

        [MaxLength(200)]
        public string PolicyName { get; set; } = null!;

        public bool IsDefault { get; set; }

        public int PolicyType { get; set; }

        public bool IsEnabled { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public ICollection<UserGroup> Groups { get; set; } = new List<UserGroup>();
    }
}
