using Cores.Dtos.UserRequests;
using Cores.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IUserRequestService _requestService;

        public RequestController(IUserRequestService requestService)
        {
            _requestService = requestService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<UserRequestResponseDto>> CreateRequest([FromBody] UserRequestCreateDto requestDto)
        {
            try
            {
                var result = await _requestService.CreateUserRequest(requestDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("summary")]
        public async Task<ActionResult<RequestSummaryDto>> GetRequestSummary(string userId)
        {

            var result = await _requestService.GetUserRequestsSummary(userId);
            return Ok(result);

        }
    }
}