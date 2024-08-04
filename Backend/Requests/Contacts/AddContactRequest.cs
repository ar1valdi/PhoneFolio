using BackendRestAPI.Model;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace BackendRestAPI.Requests.Contacts
{
    public record AddContactRequest
    {
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string Category { get; init; }
        public string? Subcategory { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime BirthDate { get; init; }

        [JsonConstructor]
        public AddContactRequest(string name, string surname, string email, string password, string category, string? subcategory, string phoneNumber, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Category = category;
            Subcategory = subcategory;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }
    }
}
