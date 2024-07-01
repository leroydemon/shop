using DbLevel.Data;
using DbLevel.Models;
using DbLevel.Repository;
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
                var repository = new UserRepository(context);
                var user = new User { Id = Guid.NewGuid().ToString(), UserName = "TestUser", Email = "test@example.com" };

                var result = await repository.AddAsync(user);

                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(user);

                var addedUser = await context.Users.FindAsync(user.Id);
                addedUser.Should().NotBeNull();
                addedUser.Should().BeEquivalentTo(user);
            }
        }
        [Fact]
        public async Task GetByEmailAsync_ShouldReturnCorrectUser()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new UserRepository(context);
                var user = new User { Id = Guid.NewGuid().ToString(), UserName = "TestUser", Email = "test@example.com" };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var result = await repository.GetByEmailAsync(user.Email);

                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(user);
            }
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnCorrectUser()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new UserRepository(context);
                var user = new User { Id = Guid.NewGuid().ToString(), UserName = "TestUser", Email = "test@example.com" };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                var result = await repository.GetByIdAsync(Guid.Parse(user.Id));

                result.Should().NotBeNull();
                result.Should().BeEquivalentTo(user);
            }
        }
        [Fact]
        public async Task GetSortedAsync_ShouldReturnSortedUsers()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new UserRepository(context);
                var users = new List<User>
                {
                    new User { Id = Guid.NewGuid().ToString(), UserName = "Alice", Email = "alice@example.com" },
                    new User { Id = Guid.NewGuid().ToString(), UserName = "Bob", Email = "bob@example.com" }
                };

                await context.Users.AddRangeAsync(users);
                await context.SaveChangesAsync();

                var result = await repository.GetSortedAsync("", 1, 2, "username", true);

                result.Should().HaveCount(2);
                result.First().UserName.Should().Be("Alice");
                result.Last().UserName.Should().Be("Bob");
            }
        }
        [Fact]
        public async Task DeleteAsync_ShouldRemoveUserFromContext()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new UserRepository(context);
                var user = new User { Id = Guid.NewGuid().ToString(), UserName = "TestUser", Email = "test@example.com" };

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
                var repository = new UserRepository(context);
                var users = new List<User>
                {
                    new User { Id = Guid.NewGuid().ToString(), UserName = "User1", Email = "user1@example.com" },
                    new User { Id = Guid.NewGuid().ToString(), UserName = "User2", Email = "user2@example.com" }
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
                var repository = new UserRepository(context);
                var user = new User { Id = Guid.NewGuid().ToString(), UserName = "OriginalUser", Email = "original@example.com" };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                user.UserName = "UpdatedUser";
                await repository.UpdateAsync(user);

                var updatedUser = await context.Users.FindAsync(user.Id);
                updatedUser.Should().NotBeNull();
                updatedUser.UserName.Should().Be("UpdatedUser");
            }
        }
        [Fact]
        public async Task SetOnlineAsync_ShouldSetUserOnline()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new UserRepository(context);
                var user = new User { Id = Guid.NewGuid().ToString(), UserName = "TestUser", Email = "test@example.com", IsOnline = false };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                await repository.SetOnlineAsync(user);

                var updatedUser = await context.Users.FindAsync(user.Id);
                updatedUser.Should().NotBeNull();
                updatedUser.IsOnline.Should().BeTrue();
            }
        }
        [Fact]
        public async Task SetOfflineAsync_ShouldSetUserOffline()
        {
            using (var context = new ApplicationDbContext(_dbContextOptions))
            {
                var repository = new UserRepository(context);
                var user = new User { Id = Guid.NewGuid().ToString(), UserName = "TestUser", Email = "test@example.com", IsOnline = true };

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                await repository.SetOfflineAsync(user);

                var updatedUser = await context.Users.FindAsync(user.Id);
                updatedUser.Should().NotBeNull();
                updatedUser.IsOnline.Should().BeFalse();
            }
        }
    }
}
