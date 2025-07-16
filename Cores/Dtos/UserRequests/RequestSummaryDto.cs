namespace Cores.Dtos.UserRequests
{
    public class RequestSummaryDto
    {
        public string UserId { get; set; } = null!;
        public Dictionary<string, int> StatusCounts { get; set; } = new Dictionary<string, int>();
    }
}