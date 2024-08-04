using BackendRestAPI.Model;
using System.Text.Json.Serialization;

namespace BackendRestAPI.Requests.Contacts
{
    public record ContactResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }

        [JsonConstructor]
        public ContactResponse(Guid id, string name, string surname, string email)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
        }

        public ContactResponse(Contact contact)
        {
            Id = contact.Id;
            Name = contact.Name;
            Surname = contact.Surname;
            Email = contact.Email;
        }
    }
}
