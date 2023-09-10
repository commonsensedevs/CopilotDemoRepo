using CopilotWebApiDemo.Models;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CopilotWebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryApi : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryApi(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_categoryRepository.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(_categoryRepository.Get(id));
        }
        [HttpPost]
        public IActionResult Post([FromBody] CategoryModel categoryModel)
        {
            var category = new Category
            {
                Name = categoryModel.Name,
                Description = categoryModel.Description
            };
            _categoryRepository.Add(category);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryModel categoryModel)
        {
            var category = _categoryRepository.Get(id);
            if (category != null)
            {
                category.Name = categoryModel.Name;
                category.Description = categoryModel.Description;
                _categoryRepository.Update(category);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return Ok();
        }
    
    }
}
