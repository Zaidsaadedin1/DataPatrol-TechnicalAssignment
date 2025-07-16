namespace Cores.Dtos.UserInfo
{
    public class UserInfoDto
    {
        public string UserId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public List<string> GroupsNames { get; set; } = new();

    }
}
