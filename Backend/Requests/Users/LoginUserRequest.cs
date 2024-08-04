namespace BackendRestAPI.Requests.Users
{
    public record LoginUserRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
