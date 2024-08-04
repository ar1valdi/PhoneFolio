using BackendRestAPI.Model;
using BackendRestAPI.Requests.Dictionaries;
using static BackendRestAPI.ServiceErrors.Errors;

namespace BackendRestAPI.Services.Dictionaries.DictionaryRequestMapper
{
    public class DictionaryRequestMapper : IDictionaryRequestMapper
    {
        public CategoriesResponse MapCategoryListToResponse(List<Category> categories)
        {
            List<CategoryResponse> responseList = new List<CategoryResponse>(categories.Count);
            foreach (var category in categories)
            {
                responseList.Add(new CategoryResponse(category));
            }
            return new CategoriesResponse(responseList);
        }

        public SubcategoriesResponse MapSubcategoryListToResponse(List<Subcategory> subcategories)
        {
            return new SubcategoriesResponse(subcategories);
        }
    }
}
