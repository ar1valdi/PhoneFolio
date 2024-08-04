using BackendRestAPI.Model;

namespace BackendRestAPI.Requests.Dictionaries
{
    public record SubcategoryResponse
    {
        public string Name { get; init; }

        public SubcategoryResponse(string name)
        {
            Name = name;
        }
    }
}
