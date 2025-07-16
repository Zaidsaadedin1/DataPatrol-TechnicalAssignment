using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserRequests
{
    public class UserRequestCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string UserId { get; set; } = null!;

        [Required]
        public int RequestCode { get; set; }

        [Required]
        [MaxLength(200)]
        public string Description { get; set; } = null!;
    }
}