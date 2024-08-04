using BackendRestAPI.Model;
using ErrorOr;

namespace BackendRestAPI.Repositories.Dictionaries
{
    public interface IDictionaryRepository
    {
        Task<ErrorOr<Created>> AddSubcategoryAsync(Subcategory subcategory);
        Task<ErrorOr<Created>> AddCategoryAsync(Category subcategory);
        Task<ErrorOr<Deleted>> RemoveSubcategoryAsync(string name);
        Task<ErrorOr<Deleted>> RemoveCategoryAsync(string name);
        Task<ErrorOr<Subcategory>> GetSubcategoryAsync(string name);
        Task<ErrorOr<Category>> GetCategoryAsync(string name);
        Task<ErrorOr<List<Subcategory>>> GetAllSubcategoriesAsync();
        Task<ErrorOr<List<Category>>> GetAllCategoriesAsync();
    }
}
