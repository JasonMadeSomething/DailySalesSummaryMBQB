using DailySalesSummary.Repositories;
using DailySalesSummary.Models;

namespace DailySalesSummary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllUsers();
        }

        public async Task<User> GetUser(string id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<User> CreateUser(User user)
        {
            return await _userRepository.CreateUser(user);
        }

        public async Task<User> UpdateUser(User userIn)
        {
            return await _userRepository.UpdateUser(userIn);
        }

        public async Task<bool> DeleteUser(string id)
        {
            return await _userRepository.DeleteUser(id);
        }


    }
}
