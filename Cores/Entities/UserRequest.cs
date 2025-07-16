using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cores.Entities
{
    public class UserRequest
    {
        [Key]
        public long RequestId { get; set; }

        [MaxLength(30)]
        [ForeignKey(nameof(RequestedBy))]
        public string RequestedBy { get; set; } = null!;

        public DateTime RequestDateTime { get; set; }

        public int RequestCode { get; set; }

        [MaxLength(200)]
        public string Description { get; set; } = null!;

        public int Status { get; set; }

        public DateTime? CompletionDateTime { get; set; }

        public UserInfo RequestedByUser { get; set; } = null!;
    }
}
