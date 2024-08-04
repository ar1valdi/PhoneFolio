using BackendRestAPI.Model;

namespace BackendRestAPI.Requests.Dictionaries
{
    public record SubcategoriesResponse
    {
        public List<SubcategoryResponse> subcategories { get; init; }

        public SubcategoriesResponse(List<SubcategoryResponse> subcategories)
        {
            this.subcategories = subcategories;
        }
        public SubcategoriesResponse(List<Subcategory> subcategories)
        {
            this.subcategories = new List<SubcategoryResponse>(subcategories.Count);
            foreach (var subcategory in subcategories)
            {
                if (!subcategory.Name.Equals(Consts.NoSubcategoryName))
                {
                    this.subcategories.Add(new SubcategoryResponse(subcategory.Name));
                }
            }
        }
    }
}
