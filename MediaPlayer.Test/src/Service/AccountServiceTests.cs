using MediaPlayer.Domain.src.UserAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Test.src.Service
{
    public class AccountServiceTests
    {
        private readonly AccountService _accountService;
        private readonly List<Account> _accounts;

        public AccountServiceTests()
        {
            _accounts = new List<Account>();
            _accountService = new AccountService(_accounts);
        }

        [Fact]
        public void GetAccountById_ShouldReturnAccount_WhenIdExists()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var account = new Account { AccountId = accountId, UserName = "testuser", Password = "password", Role = "User" };
            _accounts.Add(account);

            // Act
            var result = _accountService.GetAccountById(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(accountId, result.AccountId);
        }

        [Fact]
        public void GetAccountById_ShouldReturnNull_WhenIdDoesNotExist()
        {
            // Arrange
            var accountId = Guid.NewGuid();

            // Act
            var result = _accountService.GetAccountById(accountId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void AddAccount_ShouldAddAccount_WhenUserNameIsUnique()
        {
            // Arrange
            var account = new Account { AccountId = Guid.NewGuid(), UserName = "uniqueuser", Password = "password", Role = "User" };

            // Act
            _accountService.AddAccount(account);

            // Assert
            Assert.Contains(account, _accounts);
        }

        [Fact]
        public void AddAccount_ShouldThrowException_WhenUserNameIsNotUnique()
        {
            // Arrange
            var existingAccount = new Account { AccountId = Guid.NewGuid(), UserName = "existinguser", Password = "password", Role = "User" };
            _accounts.Add(existingAccount);
            var newAccount = new Account { AccountId = Guid.NewGuid(), UserName = "existinguser", Password = "password", Role = "User" };

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => _accountService.AddAccount(newAccount));
            Assert.Equal("An account with the same username already exists.", exception.Message);
        }

        [Fact]
        public void UpdateAccount_ShouldUpdateAccount_WhenAccountExists()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var account = new Account { AccountId = accountId, UserName = "testuser", Password = "password", Role = "User" };
            _accounts.Add(account);
            var updatedAccount = new Account { AccountId = accountId, UserName = "updateduser", Password = "newpassword", Role = "Admin" };

            // Act
            _accountService.UpdateAccount(updatedAccount);

            // Assert
            var result = _accountService.GetAccountById(accountId);
            Assert.Equal("updateduser", result.UserName);
            Assert.Equal("newpassword", result.Password);
            Assert.Equal("Admin", result.Role);
        }

        [Fact]
        public void UpdateAccount_ShouldDoNothing_WhenAccountDoesNotExist()
        {
            // Arrange
            var updatedAccount = new Account { AccountId = Guid.NewGuid(), UserName = "nonexistentuser", Password = "password", Role = "User" };

            // Act
            _accountService.UpdateAccount(updatedAccount);

            // Assert
            Assert.Null(_accountService.GetAccountById(updatedAccount.AccountId));
        }

        [Fact]
        public void RemoveAccountById_ShouldRemoveAccount_WhenIdExists()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var account = new Account { AccountId = accountId, UserName = "testuser", Password = "password", Role = "User" };
            _accounts.Add(account);

            // Act
            _accountService.RemoveAccountById(accountId);

            // Assert
            Assert.DoesNotContain(account, _accounts);
        }

        [Fact]
        public void RemoveAccountById_ShouldDoNothing_WhenIdDoesNotExist()
        {
            // Arrange
            var accountId = Guid.NewGuid();

            // Act
            _accountService.RemoveAccountById(accountId);

            // Assert
            Assert.Empty(_accounts);
        }

        [Fact]
        public void GetAllAccounts_ShouldReturnAllAccounts()
        {
            // Arrange
            var account1 = new Account { AccountId = Guid.NewGuid(), UserName = "user1", Password = "password1", Role = "User" };
            var account2 = new Account { AccountId = Guid.NewGuid(), UserName = "user2", Password = "password2", Role = "User" };
            _accounts.Add(account1);
            _accounts.Add(account2);

            // Act
            var result = _accountService.GetAllAccounts();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(account1, result);
            Assert.Contains(account2, result);
        }
    }
}