using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.UserInfo
{
    public class RegistrationDto
    {
        [Required]
        [MaxLength(200)]
        public string Username { get; set; } = null!;
    }
}