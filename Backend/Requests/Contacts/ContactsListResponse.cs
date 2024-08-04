using System.Collections;

namespace BackendRestAPI.Requests.Contacts
{
    public record ContactsListResponse(
        List<ContactResponse> contacts
    );
}
