using BackendRestAPI.Model;
using BackendRestAPI.Repositories.Contacts;
using BackendRestAPI.Repositories.Dictionaries;
using BackendRestAPI.ServiceErrors;
using BackendRestAPI.Services.Validations;
using ErrorOr;

namespace BackendRestAPI.Services.Contacts
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IDictionaryRepository _dictionaryRepository;
        private readonly IStringValidator _stringValidator;

        public ContactsService(
            IContactsRepository contactsRepository, 
            IDictionaryRepository dictionaryRepository,
            IStringValidator stringValidator)
        {
            _stringValidator = stringValidator;
            _contactsRepository = contactsRepository;
            _dictionaryRepository = dictionaryRepository;
        }

        public async Task<List<Error>> ValidateContactDataAsync(Contact contact, string username)
        {
            List<Error> errors = new List<Error>();
         
            // check if email is already in database, accept if ids are equal (editting contact)
            var checkEmailResult = await _contactsRepository.GetContactAsync(contact.Email);
            if (!checkEmailResult.IsError && checkEmailResult.Value.Id != contact.Id)
            {
                errors.Add(Errors.Contacts.EmailTaken);
                return errors;
            }
            else if (checkEmailResult.IsError && checkEmailResult.FirstError != Errors.Database.NotFound)
            {
                return checkEmailResult.Errors;
            }

            // check if contacts username matches users username
            if (!contact.User.Username.Equals(username))
            {
                errors.Add(Errors.RequestValidation.InvalidUsername);
                return errors;
            }

            // check if all fields are filled
            if (!errors.Contains(Errors.RequestValidation.PasswordCharacterNotAllowed))
            {
                if (string.IsNullOrWhiteSpace(contact.Name) ||
                    string.IsNullOrWhiteSpace(contact.Surname) ||
                    string.IsNullOrWhiteSpace(contact.PhoneNumber))
                {
                    errors.Add(Errors.RequestValidation.EmptyField);
                    return errors;
                }
            }

            // check if email, phone number and password are correctly formatted strings
            errors = _stringValidator.IsPassword(contact.Password);
            if (!_stringValidator.IsEmail(contact.Email))
            {
                errors.Add(Errors.RequestValidation.InvalidEmail);
            }
            if (!_stringValidator.IsPhoneNumber(contact.PhoneNumber))
            {
                errors.Add(Errors.RequestValidation.InvalidPhoneNumber);
            }

            return errors;
        }

        public async Task<ErrorOr<Created>> AddContactAsync(Contact contact)
        {
            // TODO: spawdzic czy mozna dodac podkategorie
            ErrorOr<Subcategory> getSubcategroyResult = await _dictionaryRepository.GetSubcategoryAsync(contact.Subcategory.Name);
            if (getSubcategroyResult.IsError && getSubcategroyResult.FirstError == Errors.Database.NotFound)
            {
                await _dictionaryRepository.AddSubcategoryAsync(contact.Subcategory);
            }

            return await _contactsRepository.AddContactAsync(contact);
        }

        public async Task<ErrorOr<Deleted>> DeleteContactAsync(Guid id)
        {
            ErrorOr<Deleted> result = await _contactsRepository.DeleteContactAsync(id);
            return result;
        }

        public async Task<ErrorOr<Updated>> EditContactAsync(Guid id, Contact newContactData)
        {
            newContactData.Id = id;
            return await _contactsRepository.UpdateContactAsync(newContactData);
        }

        public async Task<ErrorOr<List<Contact>>> GetAllContactsAsync()
        {
            return await _contactsRepository.GetAllContactsAsync();
        }

        public async Task<ErrorOr<Contact>> GetContactAsync(Guid id)
        {
            return await _contactsRepository.GetContactAsync(id);
        }

        public async Task<ErrorOr<ContactBasic>> GetContactBasicDataAsync(Guid id)
        {
            ErrorOr<Contact> contact = await GetContactAsync(id);
            if (contact.IsError) {
                return ErrorOr<ContactBasic>.From(contact.Errors);
            }
            return new ContactBasic(contact.Value);
        }
    }
}
