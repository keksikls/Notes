using Microsoft.EntityFrameworkCore;
using Moq;
using Notes.Data.AppDbContext;
using Notes.Data.Repository;
using Notes.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Notes.UnitTesting.RepositoryTest
{
    public class BaseRepositoryTests
    {
        private Mock<DbSet<T>> GetMockDbSet<T>(List<T> data) where T : class
        {
            var queryableData = data.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());

            return mockSet;
        }

        [Fact]
        public void GetAll_ShouldReturnAllEntities()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1,Name = "Alice",DisplayOrder = 1 },
                new Category { Id = 2,Name = "Bob", DisplayOrder = 2 }
            };

            var mockSet = GetMockDbSet(categories);
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Set<Category>()).Returns(mockSet.Object);

            var repository = new BaseRepository<Category>(mockContext.Object);

            // Act
            var result = repository.GetAll().ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, c => c.Name == "Electronics");
        }
    }
}
