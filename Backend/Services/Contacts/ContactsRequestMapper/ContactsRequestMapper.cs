using BackendRestAPI.Model;
using BackendRestAPI.Requests.Contacts;
using BackendRestAPI.ServiceErrors;
using BackendRestAPI.Services.Dictionaries;
using BackendRestAPI.Services.Users;
using ErrorOr;
using System.ComponentModel.Design;

namespace BackendRestAPI.Services.Contacts.ContactsRequestMapper
{
    public class ContactsRequestMapper : IContactsRequestMapper
    {
        private readonly IDictionariesService _dictionaryService;
        private readonly IUserService _userService;

        public ContactsRequestMapper(IDictionariesService dictionaryService, IUserService userService)
        {
            _dictionaryService = dictionaryService;
            _userService = userService;
        }
        public async Task<ErrorOr<Contact>> MapAddRequestToContactAsync(AddContactRequest request, string username)
        {
            ErrorOr<User> userResult = await _userService.GetUserAsync(username);
            ErrorOr<Subcategory> subcategoryResult = await GetDbSubcategoryFromRequest(request.Category, request.Subcategory);

            if (subcategoryResult.IsError)
            {
                return ErrorOr<Contact>.From(subcategoryResult.Errors);
            }

            if (userResult.IsError)
            {
                return ErrorOr<Contact>.From(userResult.Errors);
            }

            Contact contact = new Contact(
                Guid.NewGuid(),
                request.Name,
                request.Surname,
                request.Email,
                request.Password,
                subcategoryResult.Value,
                request.PhoneNumber,
                request.BirthDate,
                userResult.Value
            );

            return contact;
        }
        public async Task<ErrorOr<Contact>> MapEditRequestToContactAsync(EditContactRequest request, string username)
        {
            ErrorOr<User> userResult = await _userService.GetUserAsync(username);
            ErrorOr<Subcategory> subcategoryResult = await GetDbSubcategoryFromRequest(request.Category, request.Subcategory);

            if (subcategoryResult.IsError)
            {
                return ErrorOr<Contact>.From(subcategoryResult.Errors);
            }

            if (userResult.IsError)
            {
                return ErrorOr<Contact>.From(userResult.Errors);
            }

            Contact contact = new Contact(
                Guid.NewGuid(),
                request.Name,
                request.Surname,
                request.Email,
                request.Password,
                subcategoryResult.Value,
                request.PhoneNumber,
                request.BirthDate,
                userResult.Value
            );

            return contact;
        }

        public ContactsListResponse MapContactListToResponse(List<Contact> contacts)
        {
            var responseList = new List<ContactResponse>();

            foreach (var contact in contacts)
            {
                responseList.Add(new ContactResponse(contact));
            }

            return new ContactsListResponse(responseList);
        }

        public ContactResponse MapContactToContactResponse(Contact contact)
        {
            return new ContactResponse(contact);
        }

        public async Task<ErrorOr<Subcategory>> GetDbSubcategoryFromRequest(string categoryName, string? subcategoryName)
        {
            subcategoryName = subcategoryName ?? Consts.NoSubcategoryName;
            Subcategory subcategory;
            Category category;

            // check if category exists
            var getCategoryResult = await _dictionaryService.GetCategoryAsync(categoryName);
            if (getCategoryResult.IsError)
            {
                if (getCategoryResult.FirstError == Errors.Database.NotFound)
                {
                    return Errors.RequestValidation.InvalidCategory;
                }
                else
                {
                    return ErrorOr<Subcategory>.From(getCategoryResult.Errors);
                }
            }
            category = getCategoryResult.Value;

            // check if category needs subcategory, if does not, proceed with NoCateogrySubcategory
            if (!category.HasSubcategories)
            {
                subcategoryName = Consts.NoSubcategoryName;
            }

            // check if subcategory exists
            var getSubcategoryResult = await _dictionaryService.GetSubcategoryAsync(subcategoryName);
            if (!getSubcategoryResult.IsError)
            {
                // check if category and subcategory are compatible
                subcategory = getSubcategoryResult.Value;
                if (subcategory.Category != category)
                {
                    return Errors.RequestValidation.InvalidCategory;
                }
            }

            // if subcategory does not exist: create new
            else if (getSubcategoryResult.FirstError == Errors.Database.NotFound)
            {
                // check if category allows for custom subcategories
                if (!category.AllowCustomSubcategories)
                {
                    return Errors.RequestValidation.InvalidSubcategory;
                }

                subcategory = new Subcategory(subcategoryName, category, []);
            }
            else
            {
                return ErrorOr<Subcategory>.From(getSubcategoryResult.Errors);
            }

            subcategory.Category = getCategoryResult.Value;
            return subcategory;
        }

        public ContactDetailsResponse MapContactToDetailsResponse(Contact contact)
        {
            if (Consts.HideContactsPasswordOnDetailsRequest)
            {
                contact.Password = "*****";
            }
            return new ContactDetailsResponse(contact);
        }
    }
}
