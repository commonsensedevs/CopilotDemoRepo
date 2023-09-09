using CopiloDemoWebApp.Models;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace CopiloDemoWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public IActionResult Index()
        {
            var categories = _categoryRepository.GetAll();
            var categoryModels = new List<CategoryModel>();
            foreach (var category in categories)
            {
                var categoryModel = new CategoryModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description
                };
                categoryModels.Add(categoryModel);
            }
            return View(categoryModels);
        }

        //Add action method for Create Category
        public IActionResult Create()
        {
            return View();
        }

        //Add POST action method for Create Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = categoryModel.Name,
                    Description = categoryModel.Description
                };

                _categoryRepository.Add(category);
                return RedirectToAction("Index");
            }
            return View(categoryModel);
        }

        //Create Edit action method
        public IActionResult Edit(int id)
        {
            var category = _categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            var categoryModel = new CategoryModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
            return View(categoryModel);
        }
        //Add POST action method for Edit Category
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryModel categoryModel)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryRepository.Get(categoryModel.Id);
                category.Name = categoryModel.Name;
                category.Description = categoryModel.Description;

                _categoryRepository.Update(category);
                return RedirectToAction("Index");
            }
            return View(categoryModel);
        }
        public IActionResult Delete(int id)
        {
               var category = _categoryRepository.Get(id);
            if (category == null)
            {
                return NotFound();
            }
            _categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
