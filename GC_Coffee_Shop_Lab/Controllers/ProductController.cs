using GC_Coffee_Shop_Lab.Models;
using Microsoft.AspNetCore.Mvc;
using ProductDomain;
using ProductDTO;


namespace GC_Coffee_Shop_Lab.Controllers
{
    public class ProductController : Controller
    {
        private ProductInteractor db;
        public ProductController()
        {
            db = new ProductInteractor();
        }
        public IActionResult Index()
        {
            ViewBag.item = "";
            List<ProductViewModel> products = db
                .GetAll()
                .Select(p => ProductViewModel.GetVMFromDTO(p))
                .ToList();
            
            //if (TempData["Category"] != null)
            //{
            //    TempData["Category"] = null;
            //    return RedirectToAction(nameof(CategoryList));
            //}
            return View(products);
        }
        public IActionResult Details(int id)
        {
            ProductDT p = db.GetById(id);
            ProductViewModel product = ProductViewModel
                .GetVMFromDTO(p);
            if(product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
        public ActionResult Edit(int id)
        {
            ProductViewModel product = ProductViewModel.GetVMFromDTO(db.GetById(id));
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpPost] // I think its BROKEN!
        public ActionResult Edit(ProductViewModel vm)
        {
            ProductDT p = ProductViewModel.GetDTOFromVM(vm);
            db.UpdateProduct(p.Id,p);
            return RedirectToAction("Index");
        }
        
        public IActionResult CategoryList(int id)
        {
            
            ProductDT p = db.GetById(id);
            if(p == null)
            {
                return RedirectToAction("Index");
            }
            IEnumerable<ProductViewModel> products = db
                .GetProductByCategory(p)
                .Select(p=>ProductViewModel.GetVMFromDTO(p))
                .ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            ProductDT productToAdd = new ProductDT()
            {
                Name = collection["name"],
                Description = collection["description"],
                ImageUrl = collection["imageUrl"],
                Price = double.Parse(collection["price"]),
                Category = collection["category"]
            };
            db.AddProduct(productToAdd);
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            ProductViewModel product = ProductViewModel.GetVMFromDTO(db.GetById(id));
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            return View(product);
        }
        [HttpPost]
        public ActionResult Delete(ProductViewModel product)
        {
            db.DeleteProduct(product.Id);
            return RedirectToAction("Index");
        }
        
    }
}
