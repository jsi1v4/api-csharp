using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebApi.Features.v1.Users.DTOs;
using WebApi.Features.v1.Users.Models;

namespace WebApi.Features.v1.Users
{
    [ApiController]
    [Route("/api/v1.0/users")]
    public class UsersController : Controller
    {
        private UsersService _service;

        public UsersController(
            UsersService service
        )
        {
            _service = service;
        }

        /// <summary>
        /// Retorna lista de usuarios.
        /// </summary>
        /// <param name="query"></param>
        /// <response code="200">Returns the newly created item</response>
        [HttpGet("")]
        [ProducesResponseType<List<UserModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListUsers([FromQuery] ListUsersDTO query)
        {
            var users = await _service.FindUsers(query.Name);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int? id)
        {
            return Ok();
        }
    }
}
