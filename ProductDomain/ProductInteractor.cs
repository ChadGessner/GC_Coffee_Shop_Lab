using ProductRepository;
using ProductDTO;
namespace ProductDomain
{
    public class ProductInteractor
    {
        private Repository _repository;
        public ProductInteractor()
        {
            _repository= new Repository();
        }
        public ProductDT GetById(int id)
        {
            return _repository.GetSingleProduct(id);
        }
        public IEnumerable<ProductDT> GetAll()
        {
            return _repository.GetProducts();
        }
        public bool DeleteProduct(int id)
        {
            return _repository.RemoveProduct(id);
        }
        public bool AddProduct(ProductDT product)
        {
            ProductDT productToAdd = _repository.GetSingleProduct(product.Id);
            if(productToAdd!= null)
            {
                Console.WriteLine("Product Was not added because it already exists....");
                return false;
            }
            _repository.AddProduct(product);
            return true;
        }
       public bool UpdateProduct(int id, ProductDT updatedProduct)
        {
            
            return _repository.Update(id, updatedProduct);
        }
        public IEnumerable<ProductDT> GetProductByCategory(ProductDT product)
        {
            //Console.WriteLine(product.Category);
            return _repository
                .GetProducts()
                .Where(p => p.Category == product.Category);
        }
    }
}