using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<IEnumerable<User>>> Ping()
        {
            return Ok();
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
            var user = await _userService.GetUser(userIn.Id.ToString());

            if (user == null)
            {
                return NotFound();
            }

            var result = await _userService.UpdateUser(userIn);

            return Ok(result);
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<User>> Authenticate([FromBody] User user)
        {
            User authenticatedUser = await _userService.Authenticate(user.UserName, user.PasswordHash);

            if (authenticatedUser == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(authenticatedUser);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Username };
                bool result = await _userService.CreateUser(user, model.Password);

                if (result)
                {
                    return Ok(new { message = "User registered successfully!" });
                }

                
            }

            return BadRequest(ModelState);
        }
    }
}
