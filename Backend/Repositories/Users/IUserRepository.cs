using BackendRestAPI.Model;
using ErrorOr;

namespace BackendRestAPI.Repositories.Users
{
    public interface IUserRepository
    {
        Task<ErrorOr<Created>> AddUserAsync(User user);
        Task<ErrorOr<User>> GetUserAsync(string username);
        Task<ErrorOr<Deleted>> RemoveUserAsync(string username);
        Task<ErrorOr<Updated>> EditUserAsync(User user);
    }
}
