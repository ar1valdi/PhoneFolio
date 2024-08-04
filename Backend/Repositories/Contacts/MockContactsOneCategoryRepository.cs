using BackendRestAPI.Data;
using BackendRestAPI.Model;
using BackendRestAPI.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BackendRestAPI.Repositories.Contacts
{
    public class MockContactsOneCategoryRepository : IContactsRepository
    {
        static private List<Contact> _context;
        static private User user = new User("sample user", "1!ASDASDasdasd");
        static private Category work = new Category("Work");
        static private Subcategory client = new Subcategory("Client");
        static bool inited = false;

        public static void init()
        {
            work.Subcategories = new List<Subcategory> { client };
            work.Policy = CategoryPolicy.ALLOW_SUBCATEGORIES;
            _context = new List<Contact>
            {
                new Contact(Guid.NewGuid(), "Jane", "Doe", "janedoe1@gmail.com", "Pass123!@#", client, "1234567890", new DateTime(2020, 1, 1), user),
                new Contact(Guid.NewGuid(), "John", "Smith", "johnsmith1@gmail.com", "Pass456!@#", client, "2345678901", new DateTime(2021, 2, 2), user),
                new Contact(Guid.NewGuid(), "Alice", "Johnson", "alicejohnson1@gmail.com", "Pass789!@#", client, "3456789012", new DateTime(2022, 3, 3), user),
                new Contact(Guid.NewGuid(), "Bob", "Brown", "bobbrown1@gmail.com", "Pass012!@#", client, "4567890123", new DateTime(2023, 4, 4), user),
                new Contact(Guid.NewGuid(), "Charlie", "Davis", "charliedavis1@gmail.com", "Pass345!@#", client, "5678901234", new DateTime(2024, 5, 5), user),
                new Contact(Guid.NewGuid(), "David", "Evans", "davidevans1@gmail.com", "Pass678!@#", client, "6789012345", new DateTime(2025, 6, 6), user),
                new Contact(Guid.NewGuid(), "Eve", "Frank", "evefrank1@gmail.com", "Pass901!@#", client, "7890123456", new DateTime(2026, 7, 7), user),
                new Contact(Guid.NewGuid(), "Frank", "Green", "frankgreen1@gmail.com", "Pass234!@#", client, "8901234567", new DateTime(2027, 8, 8), user),
                new Contact(Guid.NewGuid(), "Grace", "Hill", "gracehill1@gmail.com", "Pass567!@#", client, "9012345678", new DateTime(2028, 9, 9), user),
                new Contact(Guid.NewGuid(), "Hank", "Ivy", "hankivy1@gmail.com", "Pass890!@#", client, "0123456789", new DateTime(2029, 10, 10), user),
                new Contact(Guid.NewGuid(), "Ivy", "Jones", "ivyjones1@gmail.com", "Pass321!@#", client, "1234567801", new DateTime(2030, 11, 11), user),
                new Contact(Guid.NewGuid(), "Jack", "King", "jackking1@gmail.com", "Pass654!@#", client, "2345678902", new DateTime(2031, 12, 12), user),
            };
            client.Contacts = _context;
            client.Category = work;
            inited = true;
        }

        public MockContactsOneCategoryRepository()
        {
            if (!inited)
            {
                init();
            }
        }

        public async Task<ErrorOr<Created>> AddContactAsync(Contact contact)
        {
            if (_context.Any(c => c.Id == contact.Id))
            {
                return Errors.Database.KeyConflict;
            }
            if (_context.Any(c => c.Email == contact.Email))
            {
                return Errors.Database.UniqueConflict;
            }
            _context.Add(contact);
            return Result.Created;
        }

        public async Task<ErrorOr<Deleted>> DeleteContactAsync(Guid id)
        {
            Contact? contact = _context.Find(c => c.Id == id);
            if (contact is null)
            {
                return Errors.Database.NotFound;
            }
            _context.Remove(contact);
            return Result.Deleted;
        }

        public async Task<ErrorOr<List<Contact>>> GetAllContactsAsync()
        {
            return _context;
        }

        public async Task<ErrorOr<Contact>> GetContactAsync(Guid id)
        {
            Contact? contact = _context.Find(c => c.Id == id);
            if (contact is null)
            {
                return Errors.Database.NotFound;
            }
            return contact;
        }

        public async Task<ErrorOr<Contact>> GetContactAsync(string email)
        {
            Contact? contact = _context.Find(c => c.Email == email);
            if (contact is null)
            {
                return Errors.Database.NotFound;
            }
            return contact;
        }

        public async Task<ErrorOr<Updated>> UpdateContactAsync(Contact newData)
        {
            Contact? contact = _context.Find(c => c.Id == newData.Id);
            if (contact is null)
            {
                return Errors.Database.NotFound;
            }
            contact.Update(newData);

            return Result.Updated;
        }
    }
}
