using ErrorOr;

namespace BackendRestAPI.ServiceErrors
{
    public static class Errors
    {
        public static class Contacts
        {
            public static Error NotFound => Error.NotFound(
                code: "Contacts.NotFound",
                description: "Contact not found"
            );
            public static Error EmailTaken => Error.Conflict(
                code: "Contacts.EmailTaken",
                description: "Contact with given email already exist"
            );
        }

        public static class Users
        {
            public static Error NotFound => Error.NotFound(
                code: "Users.NotFound",
                description: "User not found"
            );
            public static Error UsernameTaken => Error.Conflict(
                code: "Users.NotFound",
                description: "Username already exists"
            );
        }

        public static class Authorization
        {
            public static Error InvalidLoginData => Error.Unauthorized(
                code: "Authentication.InvalidLoginData",
                description: "Invalid username or password"
            );
            public static Error AccessDenied => Error.Unauthorized(
                code: "Authentication.Unathourized",
                description: "Access denied"
            );
        }

        public static class RequestValidation
        {
            public static Error EmptyField => Error.Validation(
                code: "RequestValidation.EmptyField",
                description: "One of mandatory fields is empty"
            );
            public static Error InvalidEmail => Error.Validation(
                code: "RequestValidation.InvalidEmail",
                description: "Invalid email"
            );
            public static Error InvalidPhoneNumber => Error.Validation(
                code: "RequestValidation.InvalidPhoneNumber",
                description: "Invalid phone number"
            );
            public static Error InvalidSubcategory => Error.Validation(
                code: "RequestValidation.InvalidSubcategory",
                description: "Invalid subcategory"
            ); 
            public static Error InvalidCategory => Error.Validation(
                code: "RequestValidation.InvalidCategory",
                description: "Invalid category"
            );
            public static Error PasswordTooShort => Error.Validation(
                code: "RequestValidation.PasswordTooShort",
                description: "Password is too short"
            );
            public static Error PasswordTooLong => Error.Validation(
                code: "RequestValidation.PasswordTooLong",
                description: "Password is too long"
            );
            public static Error PasswordNoNumber => Error.Validation(
                code: "RequestValidation.PasswordNoNumber",
                description: "Password does not contain a number"
            );
            public static Error PasswordNoSpecialCharacter => Error.Validation(
                code: "RequestValidation.PasswordNoSpecialCharacter",
                description: "Password does not contain a special character"
            );
            public static Error PasswordNoSmallLetter => Error.Validation(
                code: "RequestValidation.PasswordNoSmallLetter",
                description: "Password does not contain a small letter"
            );
            public static Error PasswordNoBigLetter => Error.Validation(
                code: "RequestValidation.PasswordNoBigLetter",
                description: "Password does not contain a big letter"
            );
            public static Error PasswordCharacterNotAllowed => Error.Validation(
                code: "RequestValidation.PasswordCharacterNotAllowed",
                description: "Password can contain only A-Z, a-z, numbers and one of: @$!%*?&#"
            );
            public static Error InvalidUsername => Error.Validation(
                code: "RequestValidation.InvalidUsername",
                description: "Username in request doesn't match user token"
            );
        }

        public static class Categories
        {
            public static Error CategoryNotFound => Error.NotFound(
                code: "Categories.CategoryNotFound",
                description: "Category not found"
            );
            public static Error SubcategoryNotFound => Error.NotFound(
                code: "Categories.CategoryNotFound",
                description: "Subcategory not found"
            );
        }

        public static class Database
        {
            public static Error KeyConflict => Error.Conflict(
                code: "Database.KeyConflict",
                description: "Given primary key already exists"
            );
            public static Error UniqueConflict => Error.Conflict(
                code: "Database.UniqueEmailConflict",
                description: "Email already exists"
            );
            public static Error ConcurrencyConflict => Error.Conflict(
                code: "Database.NotConcurrencyConflict",
                description: "Concurrency conflict arised"
            );
            public static Error Unexpected => Error.Unexpected(
                code: "Database.Unexpected",
                description: "Unexpected error occured"
            );
            public static Error NotFound => Error.NotFound(
                code: "Database.NotFound",
                description: "Entity not found"
            );
        }
    }
}
