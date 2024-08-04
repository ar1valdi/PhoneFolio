using BackendRestAPI.Model;
using BackendRestAPI.Requests.Users;

namespace BackendRestAPI.Services.Users.UserRequestMapper
{
    public interface IUserRequestMapper
    {
        User MapRegisterRequestToUser(RegisterUserRequest request);
        User MapLoginRequestToUser(LoginUserRequest request);
    }
}
