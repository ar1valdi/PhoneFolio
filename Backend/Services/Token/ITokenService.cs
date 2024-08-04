namespace BackendRestAPI.Services.Token
{
    public interface ITokenService
    {
        string GenerateJwtToken(string username);
        CookieOptions GetTokenCookieOptions();
    }
}
