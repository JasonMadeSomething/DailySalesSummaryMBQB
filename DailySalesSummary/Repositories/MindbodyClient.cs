﻿using DailySalesSummary.Models;
using Newtonsoft.Json;


namespace DailySalesSummary.Repositories
{
    public class MindbodyClient : IMindbodyClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public MindbodyClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpClient = clientFactory.CreateClient("Mindbody");
            _configuration = configuration;
        }

        public async Task<MindbodySalesDataBatch> GetMindbodySalesDataAsync(MindbodyDataRequest mindbodyDataRequest, User user)
        {
            var mindbodySalesData = new MindbodySalesDataBatch();
            

            mindbodySalesData.FetchedAt = DateTime.Now;

            if(user.Mindbody == null)
            {
                return new MindbodySalesDataBatch();
            }

            var mindbodySalesDataRequest = new MindbodyDataRequest
            {
                StartDate = mindbodyDataRequest.StartDate,
                EndDate = mindbodyDataRequest.EndDate,
                
            };
            
            if(_httpClient.DefaultRequestHeaders.Contains("API-Key"))
            {
                _httpClient.DefaultRequestHeaders.Remove("API-Key");
            }
            if(_httpClient.DefaultRequestHeaders.Contains("SiteId"))
            {
                _httpClient.DefaultRequestHeaders.Remove("SiteId");
            }
            _httpClient.DefaultRequestHeaders.Add("API-Key", _configuration["MindbodyAPI:ApiKey"]);
            _httpClient.DefaultRequestHeaders.Add("SiteId", user.Mindbody.StudioId);
            HttpResponseMessage response = await _httpClient.GetAsync($"sale/sales");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                MindbodySalesResponse salesResponse = JsonConvert.DeserializeObject<MindbodySalesResponse>(result);
                
                if (salesResponse == null)
                {
                    return new MindbodySalesDataBatch();
                }

                List<Sale> allSales = new List<Sale>(salesResponse.Sales);

                foreach (Sale sale in allSales)
                {
                    sale.userId = user.Id.ToString();    
                }

                int pageSize = salesResponse.PaginationResponse.PageSize;
                int totalResults = salesResponse.PaginationResponse.TotalResults;
                int currentPage = 1;
                int totalPages = (int)Math.Ceiling((double)totalResults / pageSize);

                while (currentPage < totalPages)
                {
                    currentPage++;
                    response = await _httpClient.GetAsync($"sale/sales?limit={pageSize}&offset={pageSize * (currentPage - 1)}");
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        salesResponse = JsonConvert.DeserializeObject<MindbodySalesResponse>(result);
                        allSales.AddRange(salesResponse.Sales);
                    }
                    else
                    {
                        Console.WriteLine($"Error getting Mindbody Sales Data: {response.StatusCode}");
                        return new MindbodySalesDataBatch();
                    }
                }
                
                foreach (Sale sale in allSales)
                {
                    sale.batchId = mindbodySalesData.Id;
                }

                mindbodySalesData.Sales = allSales;


            }
            else
            {
                Console.WriteLine($"Error getting Mindbody Sales Data: {response.StatusCode}");
                return new MindbodySalesDataBatch();
            }
            
            return mindbodySalesData;
        }


    }
}
