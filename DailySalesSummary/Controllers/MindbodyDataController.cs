using Microsoft.AspNetCore.Mvc;
using DailySalesSummary.Services;
using DailySalesSummary.Models;
using Microsoft.AspNetCore.Authorization;

namespace DailySalesSummary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class MindbodyDataController : ControllerBase
    {
        private readonly IMindbodyClientService _mindbodyClientService;
        
        public MindbodyDataController(IMindbodyClientService mindbodyClientService)
        {
            _mindbodyClientService = mindbodyClientService;
            
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "User")]
        public async Task<ActionResult> RetrieveSalesFromAPIForActiveUser([FromBody] MindbodyDataRequest mindbodyDataRequest)
        {
            string userId = User.FindFirst("id")?.Value;

            if (mindbodyDataRequest.StartDate == null || mindbodyDataRequest.EndDate == null)
            {
                mindbodyDataRequest.StartDate = DateTime.Now.AddDays(-1);
                mindbodyDataRequest.EndDate = DateTime.Now;
            }
            var mindbodySalesData = await _mindbodyClientService.GetMindbodySalesDataAsync(mindbodyDataRequest, userId);

            if (mindbodySalesData == null || mindbodySalesData == new MindbodySalesDataBatch())
            {
                return NotFound();
            }
            
            return Ok(mindbodySalesData);
        }


        

    }
}
