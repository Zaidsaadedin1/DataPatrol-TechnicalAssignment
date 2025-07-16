namespace Cores.Dtos.UserInfo
{
    public class RegistrationResponseDto
    {
        public string UserId { get; set; } = null!;
        public bool IsEnabled { get; set; }
    }
}