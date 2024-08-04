using System.ComponentModel.DataAnnotations;

namespace BackendRestAPI.Model
{
    public class User
    {
        public User(string username, string password, ICollection<Contact> contacts)
        {
            Username = username;
            Password = password;
            Contacts = contacts;
        }
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [Key]
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}