using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserRequests
{
    public class UserRequestSummaryRequestDto
    {
        [Required]
        [MaxLength(30)]
        public string UserId { get; set; } = null!;
    }
}
