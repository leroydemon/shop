using DbLevel;
using DbLevel.Data;
using DbLevel.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DbLevelTests.RepositoryTests
{
    public class RepositoryTest
    {
        public class RepositoryTests
        {
            private DbContextOptions<ApplicationDbContext> _dbContextOptions;

            public RepositoryTests()
            {
                _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            }
            [Fact]
            public async Task AddAsync_ShouldAddEntityToContext()
            {
                using (var context = new ApplicationDbContext(_dbContextOptions))
                {
                    var repository = new Repository<Category>(context);
                    var entity = new Category { Id = Guid.NewGuid(), Name = "Test Category" };

                    var result = await repository.AddAsync(entity);

                    result.Should().NotBeNull();
                    result.Should().BeEquivalentTo(entity);

                    var addedEntity = await context.Categories.FindAsync(entity.Id);
                    addedEntity.Should().NotBeNull();
                    addedEntity.Should().BeEquivalentTo(entity);
                }
            }
            [Fact]
            public async Task DeleteAsync_ShouldRemoveEntityFromContext()
            {
                using (var context = new ApplicationDbContext(_dbContextOptions))
                {
                    var repository = new Repository<Category>(context);
                    var entity = new Category { Id = Guid.NewGuid(), Name = "Test Category" };

                    await context.Categories.AddAsync(entity);
                    await context.SaveChangesAsync();

                    await repository.DeleteAsync(entity);

                    var deletedEntity = await context.Categories.FindAsync(entity.Id);
                    deletedEntity.Should().BeNull();
                }
            }
            [Fact]
            public async Task GetByIdAsync_ShouldReturnCorrectEntity()
            {
                using (var context = new ApplicationDbContext(_dbContextOptions))
                {
                    var repository = new Repository<Category>(context);
                    var entity = new Category { Id = Guid.NewGuid(), Name = "Test Category" };

                    await context.Categories.AddAsync(entity);
                    await context.SaveChangesAsync();

                    var result = await repository.GetByIdAsync(entity.Id);

                    result.Should().NotBeNull();
                    result.Should().BeEquivalentTo(entity);
                }
            }
            [Fact]
            public async Task UpdateAsync_ShouldUpdateEntityInContext()
            {
                using (var context = new ApplicationDbContext(_dbContextOptions))
                {
                    var repository = new Repository<Category>(context);
                    var entity = new Category { Id = Guid.NewGuid(), Name = "Original Name" };

                    await context.Categories.AddAsync(entity);
                    await context.SaveChangesAsync();

                    entity.Name = "Updated Name";
                    await repository.UpdateAsync(entity);

                    var updatedEntity = await context.Categories.FindAsync(entity.Id);
                    updatedEntity.Should().NotBeNull();
                    updatedEntity.Name.Should().Be("Updated Name");
                }
            }
        }
    }
}
