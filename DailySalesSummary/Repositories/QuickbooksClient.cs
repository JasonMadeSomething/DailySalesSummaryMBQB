using DailySalesSummary.Helpers;

namespace DailySalesSummary.Repositories
{
    public class QuickbooksClient
    {
        private readonly HttpClient _httpClient;

        public QuickbooksClient(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("Quickbooks");
        }

        
    }
}
