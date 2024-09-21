using Microsoft.AspNetCore.Mvc;

namespace WebApi.Features.v1.Users
{
    [ApiController]
    [Route("/api/v1.0/users")]
    public class UsersController : Controller
    {
        [HttpGet("")]
        public IActionResult GetListUsers()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int? id)
        {
            return Ok();
        }
    }
}
