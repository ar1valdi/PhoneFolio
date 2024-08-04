using BackendRestAPI.Data;
using BackendRestAPI.Model;
using BackendRestAPI.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BackendRestAPI.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<ErrorOr<Created>> AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Result.Created;
            }
            catch (DbUpdateConcurrencyException)
            {
                return Errors.Database.ConcurrencyConflict;
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("PRIMARY KEY"))
                {
                    return Errors.Database.KeyConflict;
                }
                return Errors.Database.Unexpected;
            }
            catch (Exception)
            {
                return Errors.Database.Unexpected;
            }
        }

        public async Task<ErrorOr<Updated>> EditUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<ErrorOr<User>> GetUserAsync(string username)
        {
            try
            {
                User? user = await _context.Users.FindAsync(username);
                if (user is null)
                {
                    return Errors.Database.NotFound;
                }
                return user;
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

        public async Task<ErrorOr<Deleted>> RemoveUserAsync(string username)
        {
            try
            {
                User? user = await _context.Users.FindAsync(username);
                if (user is null)
                {
                    return Errors.Database.NotFound;
                }
                
                _context.Users.Remove(user);
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
    }
}

