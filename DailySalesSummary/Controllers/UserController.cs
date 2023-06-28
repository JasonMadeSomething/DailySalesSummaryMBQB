using Microsoft.AspNetCore.Mvc;
using DailySalesSummary.Models;
using DailySalesSummary.Services;
using AspNetCore.Identity.MongoDbCore.Models;
using DailySalesSummary.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace DailySalesSummary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJWTGenerator _jwtGenerator;
        public UserController(IUserService userService, IJWTGenerator jWTGenerator)
        {
            _userService = userService;
            _jwtGenerator = jWTGenerator;
        }

        [HttpGet("index")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _userService.GetUsers();
            return Ok(users);
        }

        // GET: api/retreiveUser/{id}
        [HttpGet("retreiveUser/{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles ="Admin")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<ActionResult<User>> GetUser()
        {


            string activeUserId = User.FindFirst("id")?.Value;
            var user = await _userService.GetUser(activeUserId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles ="Admin")]
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
        [Authorize(AuthenticationSchemes = "Bearer")]
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
        public async Task<ActionResult<User>> Authenticate([FromBody] UserAuthenticateModel model)
        {
            User authenticatedUser = await _userService.Authenticate(model.UserName, model.Password);

            if (authenticatedUser == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }


            string token = await _jwtGenerator.GenerateJwtToken(authenticatedUser);


            UserResponseModel userResponse = new UserResponseModel
            {
                Id = authenticatedUser.Id.ToString(),
                Username = authenticatedUser.UserName,
                Token = token
            };

            return Ok(userResponse);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Username };
                if(model.Mindbody != null)
                {
                    user.Mindbody = model.Mindbody;
                }
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
