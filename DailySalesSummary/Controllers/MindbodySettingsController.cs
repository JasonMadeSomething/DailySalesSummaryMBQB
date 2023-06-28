using DailySalesSummary.Models;
using DailySalesSummary.Repositories;
using DailySalesSummary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DailySalesSummary.Controllers
{
    
    // MindbodySettingsController.cs
    [Route("api/[controller]")]
    
    [ApiController]
    public class MindbodySettingsController : ControllerBase
    {
        private readonly IMindbodySettingsService _mindbodySettingsService;
        private readonly IUserService _userService;
        public MindbodySettingsController(IMindbodySettingsService mindbodySettingsService, IUserService userService)
        {
            _mindbodySettingsService = mindbodySettingsService;
            _userService = userService;
        }


        // GET: api/MindbodySettings/{userId}
        [HttpGet("{userId}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Admin")]
        public async Task<ActionResult<MindbodySettings>> GetMindbodySettings(string userId)
        {
            User user = await _userService.GetUser(userId);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.Mindbody);
        }

        
        // PUT: api/MindbodySettings
        [HttpPut]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<ActionResult> UpdateMindbodySettings([FromBody] MindbodySettingsUpdateRequest request)
        {
            User user = await _userService.GetUser(request.UserId);

            if (user == null)
            {
                return NotFound();
            }

            user.Mindbody = request.MindbodySettings;
            bool result = await _mindbodySettingsService.SetMindbodySettings(request);

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }

}
