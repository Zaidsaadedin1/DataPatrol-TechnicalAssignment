namespace Cores.Dtos.Policies
{
    public class PolicyResponseDto
    {
        public string PolicyId { get; set; } = null!;
        public string PolicyName { get; set; } = null!;
        public bool IsDefault { get; set; }
        public int PolicyType { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public IEnumerable<string> GroupIds { get; set; } = new List<string>();
    }
}
