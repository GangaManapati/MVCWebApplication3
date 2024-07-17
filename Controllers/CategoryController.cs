using Microsoft.AspNetCore.Mvc;
using MVCWebApplication3.Models;
using MVCWebApplication3.Repository;
using Newtonsoft.Json;

namespace MVCWebApplication3.Controllers
{
    [Route("Category/[Action]")]
    public class CategoryController : Controller
    {
       Uri baseAddress = new Uri("https://localhost:44341/api/");
        private readonly HttpClient _client;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
           
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _categoryRepository = categoryRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetCategories(int pageNumber=1 , int pageSize=1111 )
        {
            // Call API endpoint to get categories
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress+"Categories/GetCategories");

            // Check if API call was successful
            if (response.IsSuccessStatusCode)
            {
                // Read response content as string
                string apiResponse = await response.Content.ReadAsStringAsync();

                // Deserialize JSON response to list of Category objects
                var categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);

                return View(categories); // Return view with categories
            }
            else
            {
                var categories = await _categoryRepository.GetCategoriesAsync(pageNumber, pageSize);
                return View(categories);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id)
        {
            
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Content("Select correct id");
            }

            return View(category);
        }
        //  /Category/CreateCategory
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepository.CreateCategoryAsync(category);
                return RedirectToAction("GetCategories"); // Redirect to another action, e.g., the list of categories
            }

            // If we got this far, something failed, redisplay form
            return View(category);
        }
    

        [HttpGet("{id}")]
        public async Task<ActionResult> UpdateCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Content("Select correct id");
            }

            return View(category);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category)
        {
            if (id != category.Id)
            {
                return Content("Select correct id");
            }
            if (ModelState.IsValid)
            {
                await _categoryRepository.UpdateCategoryAsync(id, category);
                return RedirectToAction("GetCategories");
            }
            return View(category);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Content("Select correct id");
            }

            return View(category);
        }

        [HttpPost("{id}"), ActionName("DeleteCategory")]
        public async Task<ActionResult> DeleteCategoryConfirmed(int id)
        {
            await _categoryRepository.DeleteCategoryAsync(id);
            return RedirectToAction("GetCategories");
        }

        [HttpGet]
        public async Task<ActionResult> GetActiveCategories(int pageNumber = 1, int pageSize = 1111)
        {
            var categories = await _categoryRepository.GetActiveCategoriesAsync(pageNumber, pageSize);
            return View("GetCategories", categories);
        }

        [HttpGet]
        public async Task<ActionResult> GetDeactivatedCategories(int pageNumber = 1, int pageSize = 1111)
        {
            var categories = await _categoryRepository.GetDeactivatedCategoriesAsync(pageNumber, pageSize);
            return View("GetCategories", categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ActivateCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Content("Select correct id");
            }

            return View(category);
        }

        [HttpPost("{id}"), ActionName("ActivateCategory")]
        public async Task<ActionResult> ActivateCategoryConfirmed(int id)
        {
            await _categoryRepository.ActivateCategoryAsync(id);
            return RedirectToAction("GetCategories");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> DeactivateCategory(int id)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Content("Select correct id");
            }

            return View(category);
        }

        [HttpPost("{id}"), ActionName("DeactivateCategory")]
        public async Task<ActionResult> DeactivateCategoryConfirmed(int id)
        {
            await _categoryRepository.DeactivateCategoryAsync(id);
            return RedirectToAction("GetCategories");
        }
    }
}
