using BackendRestAPI.Model;
using BackendRestAPI.Requests.Contacts;
using ErrorOr;

namespace BackendRestAPI.Services.Contacts.ContactsRequestMapper
{
    public interface IContactsRequestMapper
    {
        Task<ErrorOr<Contact>> MapAddRequestToContactAsync(AddContactRequest request, string username);
        Task<ErrorOr<Contact>> MapEditRequestToContactAsync(EditContactRequest request, string username);
        ContactResponse MapContactToContactResponse(Contact contact);
        Task<ErrorOr<Subcategory>> GetDbSubcategoryFromRequest(string categoryName, string? subcategoryName);
        ContactDetailsResponse MapContactToDetailsResponse(Contact contact);
        ContactsListResponse MapContactListToResponse(List<Contact> contacts);
    }
}
