namespace Cores.Dtos.Policies
{
    public class PolicyFormDto
    {
        public string PolicyId { get; set; } = string.Empty;
        public string PolicyName { get; set; } = string.Empty;
        public int PolicyType { get; set; }
        public bool IsDefault { get; set; }
        public bool IsEnabled { get; set; }
    }
}
