using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CopilotDemoDBContext _context;

        public CategoryRepository(CopilotDemoDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }
        public Category Get(int id)
        { 
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            
            if(category != null)
               return category;
            else
                throw new Exception("Category not found");
        }
        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public void CreateNewCategoriesList()
        {
            var categories = _context.Categories.ToList();
            var newCategories = new List<Category>();

            foreach(var category in categories)
            {
                var newCategory = new Category()
                {
                    Name = category.Name,
                    Description = category.Description
                };
                newCategories.Add(newCategory);
            }
            
        }

        public string CreateRefactorExampleMethod(Category category)
        {
            string validationErrorMessages = string.Empty;

            if (String.IsNullOrEmpty(category.Name))
            {
                validationErrorMessages += "Name is required";
            }
            if (String.IsNullOrEmpty(category.Description))
            {
                validationErrorMessages += "Description is required";
            }
            if (String.IsNullOrEmpty(validationErrorMessages))
            {
                return validationErrorMessages;
            }
            _context.Categories.Add(category);
            return "Category added successfully";

        }

    }
}
