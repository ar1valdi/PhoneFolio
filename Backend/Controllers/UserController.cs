using BackendRestAPI.Model;
using BackendRestAPI.Requests.Users;
using BackendRestAPI.ServiceErrors;
using BackendRestAPI.Services.Token;
using BackendRestAPI.Services.Users;
using BackendRestAPI.Services.Users.UserRequestMapper;
using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackendRestAPI.Controllers
{
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;
        private readonly IUserRequestMapper _userRequestMapper;

        public UserController(IUserService userService, IUserRequestMapper userRequestMapper, ITokenService tokenService)
        {
            _userService = userService;
            _userRequestMapper = userRequestMapper;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
        {
            // map request
            User newUser = _userRequestMapper.MapRegisterRequestToUser(request);
            
            // add user to db
            var addResult = await _userService.AddUserAsync(newUser);
            return addResult.Match(
                result => Created(),
                error => Problem(addResult.Errors)
            );
        }

        [HttpGet("username")]
        [Authorize]
        public async Task<IActionResult> GetUsername()
        {
            string? username = User.FindFirst(ClaimTypes.Name)?.Value;
            if (username == null)
            {
                return Problem([ Errors.Authorization.AccessDenied ]);
            }
            return Ok(username);
        }

        [HttpPost("jwt")]
        public async Task<IActionResult> Login(LoginUserRequest request)
        {
            // map request
            User loginData = _userRequestMapper.MapLoginRequestToUser(request);

            // verify login data
            var verificationResult = await _userService.VerifyUser(loginData);
            if (verificationResult.IsError) {
                return Problem(verificationResult.Errors);
            }
            
            if (verificationResult.Value)
            {
                // create jwt token on httponly cookie
                var token = _tokenService.GenerateJwtToken(loginData.Username);
                var cookieOptions = _tokenService.GetTokenCookieOptions();

                // send it back to client
                Response.Cookies.Append("token", token, cookieOptions);
                return Ok();
            }

            List<Error> errs = [Errors.Authorization.InvalidLoginData];
            return Problem(errs);
        }

        [HttpDelete("jwt")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(-1)
            };

            Response.Cookies.Append("token", "", cookieOptions);
            return Ok();
        }
    }
}
