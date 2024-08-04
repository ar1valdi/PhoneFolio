using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BackendRestAPI.Model
{
    public class Subcategory
    {
        public Subcategory(string name, Category category, ICollection<Contact> contacts)
        {
            Name = name;
            Category = category;
            Contacts = contacts;
        }

        public Subcategory(string name)
        {
            Name = name;
            Contacts = [];
        }

        [Key] 
        public string Name { get; set; }
        public Category Category { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
