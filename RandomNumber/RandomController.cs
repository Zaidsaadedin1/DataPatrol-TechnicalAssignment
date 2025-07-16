using Microsoft.AspNetCore.Mvc;

namespace RandomNumberApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RandomController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetRandomNumber()
        {
            var random = new Random();
            var number = random.Next(1, 100);

            var result = new
            {
                Data = new
                {
                    Number = number
                }
            };

            return Ok(result);
        }
    }
}
