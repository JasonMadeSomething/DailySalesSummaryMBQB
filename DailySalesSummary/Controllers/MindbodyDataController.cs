using Microsoft.AspNetCore.Mvc;
using DailySalesSummary.Services;
using DailySalesSummary.Models;
namespace DailySalesSummary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MindbodyDataController : ControllerBase
    {
        private readonly IMindbodyClientService _mindbodyClientService;
        private readonly IMindbodySettingsService _mindbodySettingsService;
        public MindbodyDataController(IMindbodyClientService mindbodyClientService, IMindbodySettingsService mindbodySettingsService)
        {
            _mindbodyClientService = mindbodyClientService;
            _mindbodySettingsService = mindbodySettingsService;
        }

        [HttpPost]
        public async Task<ActionResult> RetrieveSalesFromAPI([FromBody] MindbodyDataRequest mindbodyDataRequest)
        {
            
            if (mindbodyDataRequest.StartDate == null || mindbodyDataRequest.EndDate == null)
            {
                mindbodyDataRequest.StartDate = DateTime.Now.AddDays(-1);
                mindbodyDataRequest.EndDate = DateTime.Now;
            }
            mindbodyDataRequest.MindbodySettings = await _mindbodySettingsService.GetMindbodySettings(mindbodyDataRequest.UserId);
            var mindbodySalesData = await _mindbodyClientService.GetMindbodySalesDataAsync(mindbodyDataRequest);

            if (mindbodySalesData == null || mindbodySalesData == new MindbodySalesDataBatch())
            {
                return NotFound();
            }
            
            return Ok(mindbodySalesData);
        }

    }
}
