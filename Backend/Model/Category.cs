using System.ComponentModel.DataAnnotations;

namespace BackendRestAPI.Model
{
    public class Category
    {
        public Category(string name, ICollection<Subcategory> subcategories, CategoryPolicy allowCustomSubcategories)
        {
            Name = name;
            Subcategories = subcategories;
            Policy = allowCustomSubcategories;
        }
        public Category(string name)
        {
            Name = name;
            Policy = CategoryPolicy.ALLOW_SUBCATEGORIES;
        }

        [Key]
        public string Name { get; set; }
        public CategoryPolicy Policy { get; set; }
        public bool AllowCustomSubcategories => Policy == CategoryPolicy.CUSTOM_SUBCATEGORIES;
        public bool HasSubcategories => Policy != CategoryPolicy.BLOCK_SUBCATEGORIES;
        public ICollection<Subcategory> Subcategories { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}