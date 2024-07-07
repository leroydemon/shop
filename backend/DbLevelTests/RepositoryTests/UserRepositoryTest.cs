using DbLevel;
using DbLevel.Data;
using DbLevel.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace DbLevelTests.RepositoryTests
{
    public class UserRepositoryTests
    {
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public UserRepositoryTests()
        {
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
        }

        [Fact]
        public async Task AddAsync_ShouldAddUserToContext()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new Repository<User>(context);
                var user = new User { Id = Guid.NewGuid(), UserName = "TestUser", Email = "test@example.com" };

                var result = await repository.AddAsync(user);

                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(user);

                var addedUser = await context.Users.FindAsync(user.Id);
                addedUser.Should().NotBeNull();
                addedUser.Should().BeEquivalentTo(user);
            }
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectUser()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new Repository<User>(context);
                var user = new User { Id = Guid.NewGuid(), UserName = "TestUser", Email = "test@example.com" };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var result = await repository.GetByIdAsync(user.Id);

                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(user);
            }
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveUserFromContext()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new Repository<User>(context);
                var user = new User { Id = Guid.NewGuid(), UserName = "TestUser", Email = "test@example.com" };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                await repository.DeleteAsync(user);

                var deletedUser = await context.Users.FindAsync(user.Id);
                deletedUser.Should().BeNull();
            }
        }
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllUsers()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new Repository<User>(context);
                var users = new List<User>
                {
                    new User { Id = Guid.NewGuid(), UserName = "User1", Email = "user1@example.com" },
                    new User { Id = Guid.NewGuid(), UserName = "User2", Email = "user2@example.com" }
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                var result = await repository.GetAllAsync();

                result.Should().HaveCount(2);
                result.Should().BeEquivalentTo(users);
            }
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateUserInContext()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new Repository<User>(context);
                var user = new User { Id = Guid.NewGuid(), UserName = "OriginalUser", Email = "original@example.com" };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                user.UserName = "UpdatedUser";
                await repository.UpdateAsync(user);

                var updatedUser = await context.Users.FindAsync(user.Id);
                updatedUser.Should().NotBeNull();
                updatedUser.UserName.Should().Be("UpdatedUser");
            }
        }
    }
}
