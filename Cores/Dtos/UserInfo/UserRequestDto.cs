namespace Cores.Dtos.UserInfo
{
    public class UserRequestDto
    {
        public long RequestId { get; set; }
        public string RequestedBy { get; set; } = null!;
        public DateTime RequestDateTime { get; set; }
        public int RequestCode { get; set; }
        public string Description { get; set; } = null!;
        public int Status { get; set; }
        public DateTime? CompletionDateTime { get; set; }
    }

}
