namespace Cores.Dtos.UserGroups
{
    public class UserGroupResponseDto
    {
        public string GroupId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IEnumerable<string> UserIds { get; set; } = new List<string>();
        public IEnumerable<string> PolicyIds { get; set; } = new List<string>();
    }
}
