using Core.Entities;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
namespace Service.testing
{
    [TestFixture]
    public class AccountRepositoryTests 
    {
       
        
            private Mock<PMSDbContext> _context;
        private Mock<ILogger<AccountRepository>> loggerMock;
            private AccountRepository accountRepository;

            [SetUp]
            public void Setup()
            {
                // Mock DbContext and Logger
                 _context = new Mock<PMSDbContext>(new DbContextOptions<PMSDbContext>());
                loggerMock = new Mock<ILogger<AccountRepository>>();

                // Create an instance of the repository with mocked dependencies
                accountRepository = new AccountRepository( _context.Object, loggerMock.Object);
            }

            [Test]
            public async Task GetAllAccounts_ShouldReturnAllAccounts()
            {
                // Arrange
                var accountsData = new List<Account>
            {
                new Account { AccountId = 1, FullName = "John Doe", UserName = "john_doe", Password = "password123", Type = "User" },
                new Account { AccountId = 2, FullName = "Jane Doe", UserName = "jane_doe", Password = "password456", Type = "Admin" }
                // Add more test data as needed
            };

                var mockSet = new Mock<DbSet<Account>>();
                mockSet.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accountsData.AsQueryable().Provider);
                mockSet.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accountsData.AsQueryable().Expression);
                mockSet.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accountsData.AsQueryable().ElementType);
                mockSet.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(() => accountsData.AsQueryable().GetEnumerator());

                 _context.Setup(m => m.Set<Account>()).Returns(mockSet.Object);

                // Act
                var result = await accountRepository.GetAllAccounts();

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(accountsData.Count, result.Count());
            }

        
        [Test]
        public async Task GetAccountById_ShouldReturnAccount()
        {
            // Arrange
            var accountId = 7; // Replace 1 with the actual int value
            var accountData = new Account { AccountId = accountId, FullName = "John Doe", UserName = "john_doe", Password = "password123", Type = "User" };

            // Assuming your GetAccountById method now expects an int, adjust the setup accordingly
            _context.Setup(m => m.Set<Account>().FindAsync(accountId)).ReturnsAsync(accountData);

            // Act
            var result = await accountRepository.GetAccountById(accountId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(accountId, result?.AccountId);
        }


        [Test]
        public async Task AddAccount_ShouldAddAccountSuccessfully()
        {
            // Arrange
            var newAccount = new Account { AccountId = 1, FullName = "New User", UserName = "new_user", Password = "new_password", Type = "User" };

             _context.Setup(m => m.Set<Account>().AddAsync(It.IsAny<Account>(), default)).ReturnsAsync((EntityEntry<Account>)null);

            // Act
            var result = await accountRepository.AddAccount(newAccount);

            // Assert
            Assert.IsTrue(result);
             _context.Verify(m => m.Set<Account>().AddAsync(It.IsAny<Account>(), default), Times.Once);
        }

        [Test]
        public async Task UpdateAccount_ShouldUpdateExistingAccountSuccessfully()
        {
            // Arrange
            var existingAccountId = 2; // Replace 1 with the actual int value
            var existingAccount = new Account { AccountId = existingAccountId, FullName = "Existing User", UserName = "existing_user", Password = "existing_password", Type = "User" };
            var updatedAccount = new Account { AccountId = existingAccountId, FullName = "Updated User", UserName = "updated_user", Password = "updated_password", Type = "Admin" };

            _context.Setup(m => m.Set<Account>().FindAsync(existingAccountId)).ReturnsAsync(existingAccount);

            // Act
            var result = await accountRepository.UpdateAccount(updatedAccount);

            // Assert
            Assert.IsTrue(result);
            _context.Verify(m => m.Entry(existingAccount).CurrentValues.SetValues(updatedAccount), Times.Once);
        }


        [Test]
        public async Task UpdateAccount_ShouldReturnFalseForNonExistingAccount()
        {
            // Arrange
            var nonExistingAccountId = 1; // Replace 1 with an actual non-existing account ID
            var updatedAccount = new Account { AccountId = nonExistingAccountId, FullName = "Updated User", UserName = "updated_user", Password = "updated_password", Type = "Admin" };

            _context.Setup(m => m.Set<Account>().FindAsync(nonExistingAccountId)).ReturnsAsync((Account)null);

            // Act
            var result = await accountRepository.UpdateAccount(updatedAccount);

            // Assert
            Assert.IsFalse(result);
            _context.Verify(m => m.Entry(It.IsAny<Account>()), Times.Never); // Ensure Entry method is not called for non-existing account
        }


        [TearDown]
            public void Teardown()
            {
                // Clean up any resources if needed
            }
        }
}

