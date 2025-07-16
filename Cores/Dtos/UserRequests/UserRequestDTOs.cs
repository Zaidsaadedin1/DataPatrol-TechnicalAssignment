using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserRequests
{
    public class UserRequestDTOs
    {
        [Required]
        [MaxLength(30)]
        public string RequestedBy { get; set; } = null!;

        [Required]
        public int RequestCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = null!;
    }
}
