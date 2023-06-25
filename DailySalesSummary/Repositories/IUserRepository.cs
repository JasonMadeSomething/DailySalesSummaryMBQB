using DailySalesSummary.Models;

namespace DailySalesSummary.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User> GetUser(string id);
        
        // User Create
        Task<User> CreateUser(User user);
        // User Update
        Task<User> UpdateUser(User userIn);
        // User Delete
        Task<bool> DeleteUser(string id);

    }
}
