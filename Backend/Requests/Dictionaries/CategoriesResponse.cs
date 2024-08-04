using BackendRestAPI.Model;
using System.Text.Json.Serialization;

namespace BackendRestAPI.Requests.Dictionaries
{
    public record CategoriesResponse(
        List<CategoryResponse> categories
    );
}
