using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using NetMap.Data.Models;
using NetMap.Data.Repositories.Interfaces;
using Xunit;

namespace NetMap.Test.Tests.Repositories
{
    public class CategoryRepositoryShould
    {
        [Fact]
        public void GetCategoryTest()
        {
            var _categoryRepository = new Mock<ICategoryRepository>();
            _categoryRepository.Setup(m => m.GetCategory(1)).Returns(new Category());

            Assert.NotNull(_categoryRepository.Object.GetCategory(1));
        }

        [Fact]
        public void GetCategoriesTest()
        {
            var _categoryRepository = new Mock<ICategoryRepository>();
            _categoryRepository.Setup(m => m.GetCategories()).Returns(new List<Category>());

            Assert.NotNull(_categoryRepository.Object.GetCategories());
        }
    }
}
