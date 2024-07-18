using Microsoft.AspNetCore.Mvc;
using MVCWebApplication3.Models;
using MVCWebApplication3.Repository;
using Newtonsoft.Json;

namespace MVCWebApplication3.Controllers
{
    [Route("Category/[Action]")]
    public class CategoryController : Controller
    {
      
        private readonly HttpClient _client;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {

            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true; // Bypass SSL checks

            _client = new HttpClient(handler);
            _client.BaseAddress = new Uri("https://localhost:7264/api");
            _categoryRepository = categoryRepository;
        }

        //  /Category/GetCategories?pageNumber=1&pageSize=1111&API=true
        [HttpGet]
        public async Task<ActionResult> GetCategories(int pageNumber=1 , int pageSize=1111 ,bool API=false)
        {
            if (API)
            {
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Categories/GetCategories");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<List<Category>>(apiResponse);

                    return View(categories); // Return view with categories
                }
                else
                {

                     return Content("Problem with responce from Api");

                }
            }
            else
            { 
                var categories = await _categoryRepository.GetCategoriesAsync(pageNumber, pageSize);
                return View(categories);
            }
            
        }
        //     /Category/GetCategoryById/16?API=true
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById(int id,bool API=false)
        {
            if (API)
            {
                //int ID = id;
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/Categories/GetCategoryById/{id}");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<Category>(apiResponse);

                    return View(categories); // Return view with categories
                }
                else
                

                    return Content("Problem with responce from Api");

                
            }
            else
            {

                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                
                    return Content("Select correct id");
                
                else
                    return View(category);
            }
        }
        //  Category/CreateCategory?A=true
        [HttpGet]
        public ActionResult CreateCategory(bool A)
{
            TempData["API"] = A; // Store API value in TempData
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            bool API = false; // Default value if TempData["API"] is not set or not bool
            if (TempData.ContainsKey("API") && TempData["API"] is bool tempAPI)
            {
                API = tempAPI;
            }
            if (API)
            {
                
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/Categories/CreateCategory");

                if (response.IsSuccessStatusCode)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    var categories = JsonConvert.DeserializeObject<Category>(apiResponse);

                    return RedirectToAction("GetCategories"); // Return view with categories
                }
                else


                    return Content("Problem with responce from Api");


            }
            else
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepository.CreateCategoryAsync(category);
                    return RedirectToAction("GetCategories"); // Redirect to another action, e.g., the list of categories
                }

                // If we got this far, something failed, redisplay form
                return View(category);
            }
        }
    

        [HttpGet("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, bool API = false)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Content("Select correct id");
            }

            return View(category);
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, Category category, bool API = false)
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
        public async Task<ActionResult> DeleteCategory(int id, bool API = false)
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
            return View(categories);
        }

        [HttpGet]
        public async Task<ActionResult> GetDeactivatedCategories(int pageNumber = 1, int pageSize = 1111)
        {
            var categories = await _categoryRepository.GetDeactivatedCategoriesAsync(pageNumber, pageSize);
            return View(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> ActivateCategory(int id, bool API = false)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Content("Select correct id");
            }

            return View(category);
        }

        [HttpPost("{id}"), ActionName("ActivateCategory")]
        public async Task<ActionResult> ActivateCategoryConfirmed(int id, bool API = false)
        {
            await _categoryRepository.ActivateCategoryAsync(id);
            return RedirectToAction("GetCategories");
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> DeactivateCategory(int id, bool API = false)
        {
            var category = await _categoryRepository.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return Content("Select correct id");
            }

            return View(category);
        }

        [HttpPost("{id}"), ActionName("DeactivateCategory")]
        public async Task<ActionResult> DeactivateCategoryConfirmed(int id, bool API = false)
        {
            await _categoryRepository.DeactivateCategoryAsync(id);
            return RedirectToAction("GetCategories");
        }
    }
}
