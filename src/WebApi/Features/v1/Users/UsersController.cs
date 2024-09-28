using Microsoft.AspNetCore.Mvc;
using WebApi.Features.v1.Users.DTOs;
using WebApi.Features.v1.Users.Models;

namespace WebApi.Features.v1.Users
{
    [ApiController]
    [Route("/v1/users")]
    public class UsersController : Controller
    {
        private readonly UsersService _service;

        public UsersController(
            UsersService service
        )
        {
            _service = service;
        }

        /// <summary>
        /// Retorna lista de usuarios.
        /// </summary>
        [HttpGet("")]
        [ProducesResponseType<List<UserModel>>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListUsers([FromQuery] ListUsersDTO query)
        {
            var users = await _service.FindUsers(query.Name);

            return Ok(users);
        }

        /// <summary>
        /// Retorna um usuario com base no ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType<UserModel>(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _service.FindUniqueUser(id);

            return Ok(user);
        }
    }
}
