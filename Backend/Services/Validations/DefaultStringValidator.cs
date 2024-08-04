using BackendRestAPI.ServiceErrors;
using ErrorOr;
using System.Text.RegularExpressions;

namespace BackendRestAPI.Services.Validations
{
    public class DefaultStringValidator : IStringValidator
    {
        // contacts password specification
        private readonly int minPasswordLen = Consts.MinPasswordLen;
        private readonly int maxPasswordLen = Consts.MaxPasswordLen;
        private readonly bool requireSpecialCharInPassword = Consts.RequireSpecialCharInPassword;
        private readonly bool requireNumberInPassword = Consts.RequireNumberInPassword;
        private readonly bool requireSmallLetterInPassword = Consts.RequireSmallLetterInPassword;
        private readonly bool requireBigLetterInPassword = Consts.RequireBigLetterInPassword;
        private readonly string specialCharacters = Consts.SpecialCharacters;

        public bool IsEmail(string email)
        {
            // must match *@*.*
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public List<Error> IsPassword(string password)
        {
            List<Error> errors = new List<Error>();
            if (string.IsNullOrWhiteSpace(password))
            {
                errors.Add(Errors.RequestValidation.EmptyField);
                return errors;
            }


            // set presence flags to true if conditon is not required
            bool smallLetterPresent = !requireSmallLetterInPassword;
            bool bigLetterPresent = !requireBigLetterInPassword;
            bool numberPresent = !requireNumberInPassword;
            bool specialCharPresent = !requireSpecialCharInPassword;
            bool invalidCharPresent = false;
            

            // check length
            if (password.Length < minPasswordLen) { errors.Add(Errors.RequestValidation.PasswordTooShort); }
            else if (password.Length > maxPasswordLen) { errors.Add(Errors.RequestValidation.PasswordTooLong); }


            // check presence of single characters
            foreach (char c in password) {

                if (char.IsLower(c)) { smallLetterPresent = true; }
                else if (char.IsUpper(c)) { bigLetterPresent = true; }
                else if (char.IsDigit(c)) { numberPresent = true; }
                else if (specialCharacters.IndexOf(c) != -1) { specialCharPresent = true; }
                else { invalidCharPresent = true; }

            }

            if (!smallLetterPresent) { errors.Add(Errors.RequestValidation.PasswordNoSmallLetter); }
            if (!bigLetterPresent) { errors.Add(Errors.RequestValidation.PasswordNoBigLetter); }
            if (!numberPresent) { errors.Add(Errors.RequestValidation.PasswordNoNumber); }
            if (!specialCharPresent) { errors.Add(Errors.RequestValidation.PasswordNoSpecialCharacter); }
            if (invalidCharPresent) { errors.Add(Errors.RequestValidation.PasswordCharacterNotAllowed); }

            return errors;
        }

        public bool IsPhoneNumber(string phoneNumber)
        {
            if (phoneNumber.Length > 30 | phoneNumber.Length < 5)
            {
                return false;
            }
            if (phoneNumber[0] == '+')
            {
                phoneNumber = phoneNumber.Remove(0,1);
            }
            return phoneNumber.All(c => char.IsDigit(c) || c == ' ' || c == '-');
        }
    }
}
