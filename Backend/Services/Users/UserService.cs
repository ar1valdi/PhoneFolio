using BackendRestAPI.Model;
using BackendRestAPI.Repositories.Users;
using BackendRestAPI.ServiceErrors;
using BackendRestAPI.Services.Validations;
using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BackendRestAPI.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IStringValidator _stringValidator;

        public UserService(IUserRepository repository, IStringValidator stringValidator)
        {
            _repository = repository;
            _stringValidator = stringValidator;
        }

        public async Task<ErrorOr<Created>> AddUserAsync(User user)
        {
            // check if username already exists
            var checkUsernameResult = await _repository.GetUserAsync(user.Username);
            if (checkUsernameResult.IsError && checkUsernameResult.FirstError != Errors.Database.NotFound)
            {
                return ErrorOr<Created>.From(checkUsernameResult.Errors);
            }
            else if (!checkUsernameResult.IsError)
            {
                return Errors.Users.UsernameTaken;
            }

            // check if password matches requirements
            List<Error> errors = _stringValidator.IsPassword(user.Password);
            if (errors.Count > 0)
            {
                return ErrorOr<Created>.From(errors);
            }

            // hash password
            user.Password = HashUserPassword(user);

            return await _repository.AddUserAsync(user);
        }

        public async Task<ErrorOr<Updated>> EditUserAsync(User user)
        {
            return await _repository.EditUserAsync(user);
        }

        public async Task<ErrorOr<User>> GetUserAsync(string username)
        {
            return await _repository.GetUserAsync(username);
        }

        public async Task<ErrorOr<Deleted>> RemoveUserAsync(string username)
        {
            return await _repository.RemoveUserAsync(username);
        }

        public string HashUserPassword(User user) {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            return hasher.HashPassword(user, user.Password);
        }

        public async Task<ErrorOr<bool>> VerifyUser(User toVerify)
        {
            var getUserResult = await _repository.GetUserAsync(toVerify.Username);
            if (getUserResult.IsError)
            {
                if (getUserResult.FirstError == Errors.Database.NotFound)
                {
                    return false;
                }
                return ErrorOr<bool>.From(getUserResult.Errors);
            }
            if (VerifyUsersPassword(toVerify, getUserResult.Value.Password))
            {
                return true;
            }
            return false;
        }

        public bool VerifyUsersPassword(User toVerify, string hashedOriginalPassword)
        {
            PasswordHasher<User> hasher = new PasswordHasher<User>();
            return hasher.VerifyHashedPassword(
                toVerify,
                hashedOriginalPassword,
                toVerify.Password)
                == PasswordVerificationResult.Success;
        }
    }
}
