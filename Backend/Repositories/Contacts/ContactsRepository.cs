using BackendRestAPI.Data;
using BackendRestAPI.Model;
using BackendRestAPI.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BackendRestAPI.Repositories.Contacts
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly DataContext _context;

        public ContactsRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> AddContactAsync(Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return Result.Created;
            }
            catch (DbUpdateConcurrencyException)
            {
                return Errors.Database.ConcurrencyConflict;
            }
            catch (DbUpdateException ex)
            {
                // map database error to app errors
                if (ex.InnerException != null)
                {
                    if (ex.InnerException.Message.Contains("PRIMARY KEY"))
                    {
                        return Errors.Database.KeyConflict;
                    }
                    if (ex.InnerException.Message.Contains("UNIQUE"))
                    {
                        return Errors.Database.UniqueConflict;
                    }
                }
                return Errors.Database.Unexpected;
            }
            catch (Exception)
            {
                return Errors.Database.Unexpected;
            }
        }

        public async Task<ErrorOr<Deleted>> DeleteContactAsync(Guid id)
        {
            try
            {
                // find contacts
                Contact? contact = await _context.Contacts.FindAsync(id);
                if (contact is null)
                {
                    return Errors.Database.NotFound;
                }

                // perform delete
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();

                return Result.Deleted;
            }
            catch (DbUpdateConcurrencyException)
            {
                return Errors.Database.ConcurrencyConflict;
            }
            catch (DbUpdateException)
            {
                return Errors.Database.Unexpected;
            }
            catch (Exception)
            {
                return Errors.Database.Unexpected;
            }
        }

        public async Task<ErrorOr<List<Contact>>> GetAllContactsAsync()
        {
            try
            {
                return await _context.Contacts.ToListAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Errors.Database.ConcurrencyConflict;
            }
            catch (DbUpdateException)
            {
                return Errors.Database.Unexpected;
            }
            catch (Exception ex)
            {
                return Errors.Database.Unexpected;
            }
        }

        public async Task<ErrorOr<Contact>> GetContactAsync(Guid id)
        {
            try
            {
                // get contacts with category and subcategory
                Contact? contact = await _context.Contacts
                    .Include(c => c.User)
                    .Include(c => c.Subcategory)
                    .ThenInclude(s => s.Category)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (contact is null)
                {
                    return Errors.Database.NotFound;
                }
                return contact;
            }
            catch (DbUpdateConcurrencyException)
            {
                return Errors.Database.ConcurrencyConflict;
            }
            catch (DbUpdateException)
            {
                return Errors.Database.Unexpected;
            }
            catch (Exception)
            {
                return Errors.Database.Unexpected;
            }
        }

        public async Task<ErrorOr<Contact>> GetContactAsync(string email)
        {
            try
            {
                // get contacts with category and subcategory
                Contact? contact = await _context.Contacts
                    .Include(c => c.User)
                    .Include(c => c.Subcategory)
                    .ThenInclude(s => s.Category)
                    .FirstOrDefaultAsync(c => c.Email == email);
                if (contact is null)
                {
                    return Errors.Database.NotFound;
                }
                return contact;
            }
            catch (DbUpdateConcurrencyException)
            {
                return Errors.Database.ConcurrencyConflict;
            }
            catch (DbUpdateException)
            {
                return Errors.Database.Unexpected;
            }
            catch (Exception)
            {
                return Errors.Database.Unexpected;
            }
        }

        public async Task<ErrorOr<Updated>> UpdateContactAsync(Contact newData)
        {
            try
            {
                // get contact to update
                Contact? contact = await _context.Contacts.FindAsync(newData.Id);
                if (contact is null)
                {
                    return Errors.Database.NotFound;
                }

                // update contact
                contact.Update(newData);
                await _context.SaveChangesAsync();

                return Result.Updated;
            }
            catch (DbUpdateConcurrencyException)
            {
                return Errors.Database.ConcurrencyConflict;
            }
            catch (DbUpdateException)
            {
                return Errors.Database.Unexpected;
            }
            catch (Exception)
            {
                return Errors.Database.Unexpected;
            }
        }
    }
}
