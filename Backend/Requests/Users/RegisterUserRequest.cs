namespace BackendRestAPI.Requests.Users
{
    public record RegisterUserRequest
    {
        public string Username { get; init; }
        public string Password { get; init; }
    }
}
