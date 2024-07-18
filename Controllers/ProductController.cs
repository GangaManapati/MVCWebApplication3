using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCWebApplication3.Models;
using MVCWebApplication3.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVCWebApplication3.Controllers
{
    [Route("Product/[Action]")]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categorRepository;

        public ProductController(IProductRepository productRepository, ICategoryRepository categorRepository)
        {
            _productRepository = productRepository;
            _categorRepository = categorRepository;
        }

        // GET: /Product/GetProducts
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(int pageNumber = 1, int pageSize = 1111)
        {
            var products = await _productRepository.GetProductsAsync(pageNumber, pageSize);
            return View(products); // Assumes you have a view for displaying a list of products
        }

        // GET: /Product/GetProductById/4
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductById(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return Content("Select correct id"); // Returns a plain text message if product with the specified id is not found
            }

            return View(product); // Assumes you have a view named ProductDetails.cshtml
        }

        // GET: /Product/CreateProduct
        
        [HttpGet]
        public async Task<ActionResult> CreateProduct()
        {
            var categories = await _categorRepository.GetCategoriesAsync(1,11111);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }


        // POST: Product/CreateProduct
        [HttpPost]
        public async Task<ActionResult> CreateProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productRepository.AddProductAsync(product);
                return RedirectToAction("GetProducts"); // Redirect to the action that lists products after successful creation
            }
            return View(product); // Returns the view with validation errors if ModelState is not valid
        }

        // GET: /Product/UpdateProduct/3
        [HttpGet("{id}")]
        public async Task<ActionResult> UpdateProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return Content("Select correct id");
            }

            var categories = await _categorRepository.GetCategoriesAsync(1, 1111); // Adjust parameters as needed
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(product); // Returns the view for updating the product
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return Content("Select correct id");
            }

            if (ModelState.IsValid)
            {
                await _productRepository.UpdateProductAsync(id, product);
                return RedirectToAction(nameof(GetProducts));
            }

            var categories = await _categorRepository.GetCategoriesAsync(1, 1111);
            ViewBag.Categories = new SelectList(categories, "Id", "Name");

            return View(product); // Returns the view with validation errors if ModelState is not valid
        }

        // GET: /Product/DeleteProduct/3
        [HttpGet("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return Content("Select correct id");
            }

            return View(product); // Returns the view for deleting the product
        }

        // POST: /Product/DeleteProduct/3
        [HttpPost("{id}"), ActionName("DeleteProduct")]
        public async Task<ActionResult> DeleteProductConfirmed(int id)
        {
            await _productRepository.DeleteProductAsync(id);
            return RedirectToAction("GetProducts"); // Redirects to the action that lists products
        }

        // GET: /Product/GetActiveProducts?pageNumber=1&pageSize=1111
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetActiveProducts(int pageNumber = 1, int pageSize = 1111)
        {
            var products = await _productRepository.GetAllActivateProductAsync(pageNumber, pageSize);
            return View(products);
        }

        // GET: /Product/GetDeactivatedProducts?pageNumber=1&pageSize=1111
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetDeactivatedProducts(int pageNumber = 1, int pageSize = 1111)
        {
            var products = await _productRepository.GetAllDeactiveProAsync(pageNumber, pageSize);
            return View(products);
        }

        // GET: /Product/ActivateProduct/3
        [HttpGet("{id}")]
        public async Task<ActionResult> ActivateProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return Content("Select correct id");
            }

            return View(product);
        }

        // POST: /Product/ActivateProduct/3
        [HttpPost("{id}"), ActionName("ActivateProduct")]
        public async Task<ActionResult> ActivateProductConfirmed(int id)
        {
            await _productRepository.ActivateProductAsync(id);
            return RedirectToAction("GetProducts");
        }

        // GET: /Product/DeactivateProduct/3
        [HttpGet("{id}")]
        public async Task<ActionResult> DeactivateProduct(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return Content("Select correct id");
            }

            return View(product);
        }

        // POST: /Product/DeactivateProduct/3
        [HttpPost("{id}"), ActionName("DeactivateProduct")]
        public async Task<ActionResult> DeactivateProductConfirmed(int id)
        {
            await _productRepository.DeactivateProductAsync(id);
            return RedirectToAction("GetProducts");
        }
    }
}

