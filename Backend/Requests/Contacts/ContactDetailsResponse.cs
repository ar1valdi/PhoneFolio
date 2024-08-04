using BackendRestAPI.Model;
using System.Numerics;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace BackendRestAPI.Requests.Contacts
{
    public record ContactDetailsResponse
    {
        public Guid Id { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; }
        public string Email { get; init; }
        public string Password { get; init; }
        public string Category { get; init; }
        public string? Subcategory { get; init; }
        public string PhoneNumber { get; init; }
        public DateTime BirthDate { get; init; }
        public string Username { get; init; }

        [JsonConstructor]
        public ContactDetailsResponse(Guid id, string name, string surname, string email, string password, string category, string? subcategory, string phoneNumber, DateTime birthDate, string username)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Category = category;
            Subcategory = subcategory == Consts.NoSubcategoryName ? null : subcategory;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            Username = username;
        }

        public ContactDetailsResponse(Contact contact)
        {
            string subcategory = contact.Subcategory.Name;
            string category = contact.Subcategory.Category.Name;

            Id = contact.Id;
            Name = contact.Name;
            Surname = contact.Surname;
            Email = contact.Email;
            Password = contact.Password;
            Category = category;
            Subcategory = subcategory == Consts.NoSubcategoryName ? null : subcategory;
            PhoneNumber = contact.PhoneNumber;
            BirthDate = contact.BirthDate;
            Username = contact.User.Username;
        }
    }


}
