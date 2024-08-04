using BackendRestAPI.Model;
using BackendRestAPI.Requests;
using ErrorOr;

namespace BackendRestAPI.Services.Contacts
{
    public interface IContactsService
    {
        Task<ErrorOr<Created>> AddContactAsync(Contact contact);
        Task<ErrorOr<Deleted>> DeleteContactAsync(Guid id);
        Task<ErrorOr<Updated>> EditContactAsync(Guid id, Contact newContactData);
        Task<ErrorOr<List<Contact>>> GetAllContactsAsync();
        Task<ErrorOr<Contact>> GetContactAsync(Guid id);
        Task<ErrorOr<ContactBasic>> GetContactBasicDataAsync(Guid id);
        Task<List<Error>> ValidateContactDataAsync(Contact contact, string username);
    }
}
