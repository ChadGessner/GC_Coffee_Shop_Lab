using ProductDTO;

namespace GC_Coffee_Shop_Lab.Models
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string Category { get; set; }
        public static ProductDT GetProductDTFromCollection(IFormCollection collection)
        {
            return new ProductDT()
            {
                Id = int.Parse(collection["Id"]),
                Name = collection["Name"],
                Description = collection["Description"],
                ImageUrl = collection["ImageUrl"],
                Price = double.Parse(collection["Price"]),
                Category = collection["Category"]
            };

        }

        public static ProductViewModel GetVMFromDTO(ProductDT product)
        {
            return new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Category = product.Category,
            };
        }
        public static ProductDT GetDTOFromVM(ProductViewModel product)
        {
            
            return new ProductDT()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Category = product.Category,
            };
        }
        public static string GetCategoryName(ProductViewModel thisVM)
        {
            return thisVM.Category;
        }
    }
}
