using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BackendRestAPI.Model
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Subcategory Subcategory { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime BirthDate { get; set; }
        public User User { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Contact(Guid id,
                       string name,
                       string surname,
                       string email,
                       string password,
                       string phoneNumber,
                       DateTime birthDate)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
        }

        public Contact(Guid id,
                       string name,
                       string surname,
                       string email,
                       string password,
                       Subcategory subcategory,
                       string phoneNumber,
                       DateTime birthDate,
                       User user)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Password = password;
            Subcategory = subcategory;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            User = user;
        }

        public void Update(Contact c)
        {
            Id = c.Id;
            Name = c.Name;
            Surname = c.Surname;
            Email = c.Email;
            Password = c.Password;
            Subcategory = c.Subcategory;
            PhoneNumber = c.PhoneNumber;
            BirthDate = c.BirthDate;
            User = c.User;
        }
    }
}
