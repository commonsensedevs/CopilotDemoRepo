using Moq;
using System.Collections.Generic;
using CopilotWebApiDemo.Controllers;
using CopilotWebApiDemo.Models;
using Core;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Microsoft.AspNetCore.Http;

namespace CategoryApiTests
{
    public class CategoryApiTests
    {
        [Fact]
        public void GetAll_Returns_AllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Name="Beverages" },
                new Category { Name="Food" }
            };
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.GetAll())
                .Returns(categories);
            var controller = new CategoryApi(mockRepo.Object);

            // Act
            var result = controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCategories = Assert.IsAssignableFrom<IEnumerable<Category>>(okResult.Value);
            var list = new List<Category>(returnCategories);
            Assert.Equal(2, list.Count);
            Assert.Equal("Beverages", list[0].Name);
            Assert.Equal("Food", list[1].Name);
        }

        [Fact]
        public void Get_Returns_CorrectCategory()
        {
            // Arrange
            int categoryId = 1;
            var category = new Category { Name="Beverages" };
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.Get(categoryId))
                .Returns(category);
            var controller = new CategoryApi(mockRepo.Object);

            // Act
            var result = controller.Get(categoryId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnCategory = Assert.IsType<Category>(okResult.Value);
            Assert.Equal("Beverages", returnCategory.Name);
        }


        [Fact]
        public void Put_UpdatesCategory()
        {
            //Arrange
            int categoryId = 1;
            var categoryModel = new CategoryModel { Name = "Beverages", Description = "Drinks" };
            var category = new Category { Id = categoryId, Name = categoryModel.Name, Description = categoryModel.Description };
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.Get(categoryId))
                .Returns(category);
            mockRepo.Setup(repo => repo.Update(category));
            var controller = new CategoryApi(mockRepo.Object);

            //Act
            var result = controller.Put(categoryId, categoryModel);

            //Assert
            Assert.IsType<OkResult>(result);
            mockRepo.VerifyAll();
        }

        [Fact]
        public void Put_Returns_NotFoundWhenCategoryIdIsInvalid()
        {
            //Arrange
            int categoryId = 1;
            var categoryModel = new CategoryModel { Name = "Beverages", Description = "Drinks" };
            Category category = null;
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.Get(categoryId))
                .Returns(category);
            var controller = new CategoryApi(mockRepo.Object);

            //Act
            var result = controller.Put(categoryId, categoryModel);

            //Assert
            var notFoundResult = Assert.IsType<NotFoundResult>(result);
            mockRepo.VerifyAll();
        }

        [Fact]
        public void Delete_RemovesCategory()
        {
            //Arrange
            int categoryId = 1;
            var mockRepo = new Mock<ICategoryRepository>();
            mockRepo.Setup(repo => repo.Delete(categoryId));
            var controller = new CategoryApi(mockRepo.Object);

            //Act
            var result = controller.Delete(categoryId);

            //Assert
            Assert.IsType<OkResult>(result);
            mockRepo.VerifyAll();
        }
    }
}
