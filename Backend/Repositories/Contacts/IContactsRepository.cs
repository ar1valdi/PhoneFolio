using BackendRestAPI.Model;
using ErrorOr;

namespace BackendRestAPI.Repositories.Contacts
{
    public interface IContactsRepository
    {
        Task<ErrorOr<List<Contact>>> GetAllContactsAsync();
        Task<ErrorOr<Contact>> GetContactAsync(Guid id);
        Task<ErrorOr<Contact>> GetContactAsync(string email);
        Task<ErrorOr<Created>> AddContactAsync(Contact contact);
        Task<ErrorOr<Updated>> UpdateContactAsync(Contact contact);
        Task<ErrorOr<Deleted>> DeleteContactAsync(Guid id);
    }
}
