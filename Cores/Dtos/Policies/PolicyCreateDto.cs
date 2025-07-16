using System.ComponentModel.DataAnnotations;

namespace Cores.Dtos.Policies
{
    public class PolicyCreateDto
    {
    [Required]
    [MaxLength(30)]
    public string PolicyId { get; set; } = null!;

    [Required]
    [MaxLength(200)]
    public string PolicyName { get; set; } = null!;

    public bool IsDefault { get; set; }
    public int PolicyType { get; set; }
    public bool IsEnabled { get; set; }
}
}
