﻿using DailySalesSummary.Models;

namespace DailySalesSummary.Services
{
    public interface IMindbodyClientService
    {
        Task<IEnumerable<Sale>> GetMindbodySalesDataAsync(MindbodyDataRequest mindbodyDataRequest, string userid);

        Task<MindbodyBatchReport> RunBatchForAllUsers(string triggeringUserId);
    }
}
