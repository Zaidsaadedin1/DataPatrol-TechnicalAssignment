using System.ComponentModel.DataAnnotations;

namespace Cores.Entities
{
    public class UserInfo
    {
        [Key]
        [MaxLength(30)]
        public string UserId { get; set; } = null!;

        [MaxLength(200)]
        public string Username { get; set; } = null!;

        public bool IsEnabled { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public ICollection<UserGroup> Groups { get; set; } = new List<UserGroup>();

        public ICollection<UserRequest> Requests { get; set; } = new List<UserRequest>();
    }
}
