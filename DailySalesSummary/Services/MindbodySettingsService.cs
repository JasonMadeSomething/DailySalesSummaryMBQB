﻿using DailySalesSummary.Models;
using DailySalesSummary.Repositories;

namespace DailySalesSummary.Services
{
    public class MindbodySettingsService : IMindbodySettingsService
    {
        private readonly IUserRepository _userRepository;

        public MindbodySettingsService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<MindbodySettings> GetMindbodySettings(string userId)
        {
            var user = await _userRepository.GetUser(userId);
            return user?.Mindbody;
        }

        public async Task<bool> SetMindbodySettings(MindbodySettingsUpdateRequest request)
        {
            var user = await _userRepository.GetUser(request.UserId);

            if (user == null)
            {
                return false;
            }

            user.Mindbody = request.MindbodySettings;
            var result = await _userRepository.UpdateUser(user);

            return result;
        }
    }
}
