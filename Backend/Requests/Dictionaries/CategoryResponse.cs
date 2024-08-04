using BackendRestAPI.Model;
using System.Text.Json.Serialization;
using static BackendRestAPI.ServiceErrors.Errors;

namespace BackendRestAPI.Requests.Dictionaries
{
    public record CategoryResponse
    {
        public string Name { get; init; }
        public int Policy { get; init; }

        public CategoryResponse(string name, int policy)
        {
            Name = name;
            Policy = policy;
        }

        public CategoryResponse(Category category)
        {
            Name = category.Name;
            Policy = (int)category.Policy;
        }
    }
}
