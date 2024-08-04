using BackendRestAPI.Model;
using ErrorOr;

namespace BackendRestAPI.Services.Users
{
    public interface IUserService
    {
        Task<ErrorOr<User>> GetUserAsync(string username);
        Task<ErrorOr<Created>> AddUserAsync(User user);
        Task<ErrorOr<Deleted>> RemoveUserAsync(string username);
        Task<ErrorOr<Updated>> EditUserAsync(User user);
        Task<ErrorOr<bool>> VerifyUser(User toVerify);
    }
}
