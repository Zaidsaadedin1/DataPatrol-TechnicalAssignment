using Cores.Dtos.UserInfo;
using Cores.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserInfosService _userService;

        public UserController(IUserInfosService userService)
        {
            _userService = userService;
        }

        [HttpPost("reg")]
        public async Task<ActionResult<RegistrationResponseDto>> RegisterUser(string username) { 

                var result = await _userService.RegisterUserAsync(username);
                return Ok(result);
    
        }
    }
}