using BackendRestAPI.Services.Token;
using BackendRestAPI.Services.Users;

namespace BackendRestAPI.Middleware
{
    public class SlidingExpirationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenService _tokenService;

        public SlidingExpirationMiddleware(RequestDelegate next, ITokenService tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated) {

                var authHeader = context.Request.Headers["Cookie"];
                string token = authHeader.ToString().Replace("token=", "");

                if (!string.IsNullOrEmpty(token) && context.User.Identity.Name != null)
                {
                    string newToken = _tokenService.GenerateJwtToken(context.User.Identity.Name);
                    CookieOptions options = _tokenService.GetTokenCookieOptions();
                    context.Response.Cookies.Append("token", newToken, options);
                }
            }

            await _next(context);
        }
    }
}
