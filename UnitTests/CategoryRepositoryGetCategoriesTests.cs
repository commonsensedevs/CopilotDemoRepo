using Core;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class CategoryRepositoryGetCategoriesTests
    {
        [Fact]
        public void GetAllCategoriesTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CopilotDemoDBContext>()
                .UseInMemoryDatabase(databaseName: "GetAllCategoriesTest")
                .Options;

            using (var context = new CopilotDemoDBContext(options))
            {
                var categoryRepository = new CategoryRepository(context);
                var category1 = new Category()
                {
                    Name = "Test Category",
                    Description = "Test Description"
                };
                var category2 = new Category()
                {
                    Name = "Test Category",
                    Description = "Test Description"
                };

                // Act
                categoryRepository.Add(category1);
                categoryRepository.Add(category2);

                var categories = categoryRepository.GetAll();
                // Assert
                Assert.Equal(2, categories.Count());
            }

        }
    }
}
