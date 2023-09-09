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
    public class CategoryRepositoryCreateTests
    {
        [Fact]
        public void CreateNewCategoryTest()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CopilotDemoDBContext>()
                .UseInMemoryDatabase(databaseName: "CreateNewCategoryTest")
                .Options;

            using (var context = new CopilotDemoDBContext(options))
            {
                var categoryRepository = new CategoryRepository(context);
                var category = new Category()
                {
                    Name = "Test Category",
                    Description = "Test Description"
                };

                // Act
                categoryRepository.Add(category);

                // Assert
                Assert.Equal(1, context.Categories.Count());
                Assert.Equal("Test Category", context.Categories.Single().Name);
                Assert.Equal("Test Description", context.Categories.Single().Description);
            }

        }
    }
}
