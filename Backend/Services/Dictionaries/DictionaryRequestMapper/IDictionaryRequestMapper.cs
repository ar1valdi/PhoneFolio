using BackendRestAPI.Model;
using BackendRestAPI.Requests.Dictionaries;

namespace BackendRestAPI.Services.Dictionaries.DictionaryRequestMapper
{
    public interface IDictionaryRequestMapper
    {
        CategoriesResponse MapCategoryListToResponse(List<Category> categories);
        SubcategoriesResponse MapSubcategoryListToResponse(List<Subcategory> subcategories);

    }
}
