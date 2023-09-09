//Ensure to add the necessary namespaces such as Microsoft.EntityFrameworkCore and Xunit before running test code. 
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;
using DataAccessLayer;
using System.Data.Common;
using Core;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace UnitTests
{
    public class CategoryRepositoryTests
    {
        [Fact]
        public void Add_AddsUserToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<CopilotDemoDBContext>()
                .UseInMemoryDatabase(databaseName: "Add_writes_to_database")
                .Options;
            var context = new CopilotDemoDBContext(options);
            var repository = new CategoryRepository(context);

            // Act
            var category = new Category { Name = "John", Description="Desc"};
            repository.Add(category);

            // Assert
            var retrievedCategory = repository.Get(category.Id);
            Assert.NotNull(retrievedCategory);
            Assert.Equal("John", retrievedCategory.Name);
        }

    }
}