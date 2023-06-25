using Microsoft.AspNetCore.Mvc;
using DailySalesSummary.Repositories;
using DailySalesSummary.Models;
using DailySalesSummary.Services;

namespace DailySalesSummary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUser(string id)
        {
            var result = await _userService.DeleteUser(id);

            if (!result)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User userIn)
        {
            var user = await _userService.GetUser(userIn.Id);

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.UpdateUser(userIn);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateUser(user);

            return CreatedAtAction(nameof(GetUser), new { id = createdUser.Id }, createdUser);
        }

    }
}
