using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ProductDTO;

namespace ProductRepository
{
    public class Repository
    {
        private IConfigurationRoot _configuration;
        private DbContextOptionsBuilder<ApplicationDbContext> _optionsBuilder;
        public Repository()
        {
            BuildOptions();
        }
        private void BuildOptions()
        {
            _configuration = ConfigurationBuilderSingleton.ConfigurationRoot;
            _optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            _optionsBuilder.UseSqlServer(_configuration.GetConnectionString("StringyConnection"));

        }
        public bool AddProduct(ProductDT productToAdd)
        {
            using (ApplicationDbContext db = new ApplicationDbContext(_optionsBuilder.Options))
            {
                ProductDT existingProduct = db.Products.FirstOrDefault(p => p.Name == productToAdd.Name);
                if (existingProduct == null)
                {
                    db.Products.Add(productToAdd);
                    db.SaveChanges();
                    return true;
                }
                return false;
            }

        }
        //using (ApplicationDbContext db = new ApplicationDbContext(_optionsBuilder.Options))
        //{

        //}
        public bool RemoveProduct(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext(_optionsBuilder.Options))
            {
                if (db.Products.FirstOrDefault(p => p.Id == id) != null)
                {
                    ProductDT product = db.Products.FirstOrDefault(p => p.Id == id);
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return true;

                }
                return false;
            }
        }
        public IEnumerable<ProductDT> GetProducts()
        {
            using (ApplicationDbContext db = new ApplicationDbContext(_optionsBuilder.Options))
            {
                return db.Products.ToList();
            }
        }
        public ProductDT GetSingleProduct(int id)
        {
            using (ApplicationDbContext db = new ApplicationDbContext(_optionsBuilder.Options))
            {
                return db.Products.SingleOrDefault(p => p.Id == id);
            }
        }
        public bool Update(int id, ProductDT updatedProduct)
        {
            using(ApplicationDbContext db = new ApplicationDbContext(_optionsBuilder.Options))
            {
                ProductDT product = db.Products.SingleOrDefault(p => p.Id == id);
                product.Name = updatedProduct.Name;
                product.Description = updatedProduct.Description;
                product.Category = updatedProduct.Category;
                product.Price = updatedProduct.Price;
                product.ImageUrl = updatedProduct.ImageUrl;
                


                db.SaveChanges();
                return true;
            }
        }
    }
}