using BackendRestAPI.Data;
using BackendRestAPI.Model;
using BackendRestAPI.ServiceErrors;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace BackendRestAPI.Repositories.Dictionaries
{
    public class DictionaryRepository : IDictionaryRepository
    {
        private readonly DataContext _context;

        public DictionaryRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<ErrorOr<Created>> AddSubcategoryAsync(Subcategory subcategory)
        {
            try
            {
                _context.Subcategories.Add(subcategory);
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

        public async Task<ErrorOr<Created>> AddCategoryAsync(Category category)
        {
            try
            {
                _context.Categories.Add(category);
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

        public async Task<ErrorOr<Deleted>> RemoveSubcategoryAsync(string name)
        {
            try
            {
                Subcategory? subcategory = await _context.Subcategories.FindAsync(name);
                if (subcategory is null)
                {
                    return Errors.Database.NotFound;
                }
                _context.Subcategories.Remove(subcategory);
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
        public async Task<ErrorOr<Deleted>> RemoveCategoryAsync(string name)
        {
            try
            {
                Category? category = await _context.Categories.FindAsync(name);
                if (category is null)
                {
                    return Errors.Database.NotFound;
                }
                _context.Categories.Remove(category);
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

        public async Task<ErrorOr<Subcategory>> GetSubcategoryAsync(string name)
        {
            try
            {
                Subcategory? subcategory = await _context.Subcategories
                    .Include(c => c.Category)
                    .FirstOrDefaultAsync(c => c.Name.Equals(name));
                if (subcategory is null)
                {
                    return Errors.Database.NotFound;
                }
                return subcategory;
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
        public async Task<ErrorOr<Category>> GetCategoryAsync(string name)
        {
            try
            {
                Category? category = await _context.Categories
                    .Include(c => c.Subcategories)
                    .FirstOrDefaultAsync(c => c.Name == name);
                if (category is null)
                {
                    return Errors.Database.NotFound;
                }
                return category;
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

        public async Task<ErrorOr<List<Subcategory>>> GetAllSubcategoriesAsync()
        {
            try
            {
                return await _context.Subcategories.ToListAsync();
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

        public async Task<ErrorOr<List<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                return await _context.Categories.ToListAsync();
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
