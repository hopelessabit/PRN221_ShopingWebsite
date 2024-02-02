using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.testing
{
    [TestFixture]
    public class CategoryRepositoryTests
    {
        private Mock<PMSDbContext> _context;
        private Mock<DbSet<Category>> _dbSet;
        private CategoryRepository _categoryRepository;

        [SetUp]
        public void Setup()
        {
            _context = new Mock<PMSDbContext>();
            _dbSet = new Mock<DbSet<Category>>();

            _context.Setup(x => x.Set<Category>()).Returns(_dbSet.Object);

            _categoryRepository = new CategoryRepository(_context.Object, Mock.Of<ILogger<CategoryRepository>>());
        }

        [Test]
        public async Task GetAllCategories_ShouldReturnListOfCategories()
        {
            
            var categoriesData = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Category1" },
                new Category { CategoryId = 2, CategoryName = "Category2" }
            };

            _dbSet.Setup(x => x.ToListAsync(It.IsAny<CancellationToken>())).ReturnsAsync(categoriesData);

           
            var result = await _categoryRepository.GetAll();

         
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<IEnumerable<Category>>(result);
            Assert.AreEqual(categoriesData.Count, ((List<Category>)result).Count);
        }

        /*[Test]
        public async Task GetCategoryById_ShouldReturnCategory()
        {
            
            var categoryId = 1;
            var categoryData = new Category { CategoryId = categoryId, CategoryName = "Category1" };

            _dbSet.Setup(x => x.FindAsync(categoryId)).ReturnsAsync(categoryData);

           
            var result = await _categoryRepository.GetById(categoryId);

         
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryId, result?.CategoryId);
        }*/

        [Test]
        public async Task AddCategory_ShouldReturnTrue()
        {
            
            var newCategory = new Category { CategoryId = 3, CategoryName = "Category3" };

            _context.Setup(m => m.Set<Category>().AddAsync(It.IsAny<Category>(), default)).ReturnsAsync(new EntityEntry<Category>(new Mock<InternalEntityEntry>().Object));

           
            var result = await _categoryRepository.Add(newCategory);

         
            Assert.IsTrue(result);
            _dbSet.Verify(x => x.AddAsync(newCategory, It.IsAny<CancellationToken>()), Times.Once);
        }

      /*  [Test]
        public async Task RemoveCategory_ShouldReturnTrue()
        {
            // Arrange
            var categoryIdToRemove = 1;
            var categoryToRemove = new Category { CategoryId = categoryIdToRemove, CategoryName = "Category1" };

            // Assuming your Remove method expects an int, adjust the setup accordingly
            _dbSet.Setup(x => x.FindAsync(categoryIdToRemove)).ReturnsAsync(categoryToRemove);

            // Act
            var result = await _categoryRepository.Remove(categoryIdToRemove);

            // Assert
            Assert.IsTrue(result);
            _dbSet.Verify(x => x.FindAsync(categoryIdToRemove), Times.Once);
            _dbSet.Verify(x => x.Remove(categoryToRemove), Times.Once);
        }*/

        [Test]
        public async Task UpdateCategory_ShouldReturnTrue()
        {
            
            var categoryIdToUpdate = 1;
            var existingCategory = new Category { CategoryId = categoryIdToUpdate, CategoryName = "Category1" };
            var updatedCategory = new Category { CategoryId = categoryIdToUpdate, CategoryName = "UpdatedCategory" };

            _dbSet.Setup(x => x.FindAsync(categoryIdToUpdate)).ReturnsAsync(existingCategory);

           
            var result = await _categoryRepository.UpdateCategory(updatedCategory);

         
            Assert.IsTrue(result);
            _dbSet.Verify(x => x.FindAsync(categoryIdToUpdate), Times.Once);
            _context.Verify(x => x.Entry(existingCategory).CurrentValues.SetValues(updatedCategory), Times.Once);
        }


    }
}
