using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserRequests
{
    public class UserRequestUpdateDto
    {
        [Key]
        public long RequestId { get; set; }

        [Required]
        public int RequestCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = null!;

        [Required]
        public int Status { get; set; }
    }
}
