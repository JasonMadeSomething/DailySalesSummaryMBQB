using DailySalesSummary.Models;
namespace DailySalesSummary.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<User> GetUser(string id);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user);

        Task<bool> DeleteUser(string id);

    }
}
