using BackendRestAPI.Model;
using BackendRestAPI.Repositories.Dictionaries;
using BackendRestAPI.ServiceErrors;
using ErrorOr;

namespace BackendRestAPI.Services.Dictionaries
{
    public class DictionariesService : IDictionariesService
    {
        private readonly IDictionaryRepository _dictionaryRepository;
        
        public DictionariesService(IDictionaryRepository dictionaryRepository)
        {
            _dictionaryRepository = dictionaryRepository;
        }

        public async Task<ErrorOr<Created>> AddSubcategoryAsync(Subcategory subcategory)
        {
            return await _dictionaryRepository.AddSubcategoryAsync(subcategory);
        }

        public async Task<ErrorOr<List<Category>>> GetAllCategoriesAsync()
        {
            return await _dictionaryRepository.GetAllCategoriesAsync();
        }

        public async Task<ErrorOr<List<Subcategory>>> GetCategorySubcategoriesAsync(string categoryName)
        {
            var getCategoryAsyncResult = await _dictionaryRepository.GetCategoryAsync(categoryName);

            // if no errors occured: return subcategories
            if (!getCategoryAsyncResult.IsError)
            {
                Category category = getCategoryAsyncResult.Value;
                return category.Subcategories.ToList();
            }

            // handle errors
            else if (getCategoryAsyncResult.FirstError == Errors.Database.NotFound)
            {
                return Errors.Categories.CategoryNotFound;
            }
            else
            {
                return ErrorOr<List<Subcategory>>.From(getCategoryAsyncResult.Errors);
            }
        }

        public async Task<ErrorOr<Category>> GetCategoryAsync(string name)
        {
            return await _dictionaryRepository.GetCategoryAsync(name);
        }

        public async Task<ErrorOr<Subcategory>> GetSubcategoryAsync(string name)
        {
            return await _dictionaryRepository.GetSubcategoryAsync(name);
        }

        public async Task<ErrorOr<Deleted>> RemoveSubcategoryAsync(string name)
        {
            return await _dictionaryRepository.RemoveSubcategoryAsync(name);
        }
    }
}
