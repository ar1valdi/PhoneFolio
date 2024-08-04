using BackendRestAPI.Model;
using ErrorOr;

namespace BackendRestAPI.Services.Dictionaries
{
    public interface IDictionariesService
    {
Task<ErrorOr<Subcategory>> GetSubcategoryAsync(string name);
Task<ErrorOr<Category>> GetCategoryAsync(string name);
Task<ErrorOr<List<Subcategory>>> GetCategorySubcategoriesAsync(string category);
Task<ErrorOr<List<Category>>> GetAllCategoriesAsync();
Task<ErrorOr<Created>> AddSubcategoryAsync(Subcategory subcategory);
Task<ErrorOr<Deleted>> RemoveSubcategoryAsync(string name);
    }
}
