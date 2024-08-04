using ErrorOr;

namespace BackendRestAPI.Services.Validations
{
    public interface IStringValidator
    {
        public bool IsEmail(string email);
        public List<Error> IsPassword(string password);
        public bool IsPhoneNumber(string phoneNumber);
    }
}
