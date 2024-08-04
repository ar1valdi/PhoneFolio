using BackendRestAPI.Model;
using BackendRestAPI.Requests.Users;
using Microsoft.AspNetCore.Identity.Data;

namespace BackendRestAPI.Services.Users.UserRequestMapper
{
    public class UserRequestMapper : IUserRequestMapper
    {
        public User MapLoginRequestToUser(LoginUserRequest request)
        {
            return new User(request.Username, request.Password);
        }

        public User MapRegisterRequestToUser(RegisterUserRequest request)
        {
            return new User(request.Username, request.Password);
        }
    }
}
