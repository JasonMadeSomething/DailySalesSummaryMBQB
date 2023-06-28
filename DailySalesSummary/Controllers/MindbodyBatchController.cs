using Microsoft.AspNetCore.Mvc;
using DailySalesSummary.Services;
using DailySalesSummary.Models;
using Microsoft.AspNetCore.Authorization;

namespace DailySalesSummary.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class MindbodyBatchController : ControllerBase
    {
        private readonly IMindbodyBatchService _mindbodyBatchService;
        public  MindbodyBatchController(IMindbodyBatchService mindbodyBatchService)
        {
            _mindbodyBatchService = mindbodyBatchService;
        }


        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "System,Admin")]
        public async Task<ActionResult> RunSalesRetrivalBatch()
        {
            MindbodyDataRequest mindbodyDataRequest = new MindbodyDataRequest();
            mindbodyDataRequest.StartDate = DateTime.Now.AddDays(-1);
            mindbodyDataRequest.EndDate = DateTime.Now;
            
            string triggeringUserId = User.FindFirst("id")?.Value;

            MindbodyBatchReport batchReport = await _mindbodyBatchService.RunBatchForAllUsers(triggeringUserId);

            if (batchReport == null || batchReport == new MindbodyBatchReport())
            {
                return NotFound();
            }

            return Ok(batchReport);
        }
    }
}
