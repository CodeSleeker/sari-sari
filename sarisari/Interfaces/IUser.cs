using System.Collections.Generic;
using System.Threading.Tasks;

namespace sarisari
{
    public interface IUser
    {
        Task<bool> IsRunning();
        Task EnsureUserStoreAsync();
        Task<User> GetUserAsync();
        Task<List<User>> GetUsersAsync();
        Task SaveUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task AddUserAsync(User user);
        Task RemoveUserAsync(User user);
    }
}
