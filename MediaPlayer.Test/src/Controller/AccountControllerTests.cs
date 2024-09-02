using MediaPlayer.Controller.src;
using MediaPlayer.Domain.src.UserAggregate;
using MediaPlayer.Service.src.Services;
using Moq;

namespace MediaPlayer.Tests
{
    public class AccountControllerTests
    {
        private readonly AccountController _accountController;
        private readonly Mock<AccountService> _mockAccountService;

        public AccountControllerTests()
        {
            _mockAccountService = new Mock<AccountService>();
            _accountController = new AccountController(_mockAccountService.Object);
        }

        [Fact]
        public void Register_ShouldAddAccount_WhenUsernameDoesNotExist()
        {
            // Arrange
            var username = "newuser";
            var password = "password";
            var role = "User";
            _mockAccountService.Setup(s => s.GetAccountByUsername(username)).Returns((Account)null);

            // Act
            _accountController.Register(username, password, role);

            // Assert
            _mockAccountService.Verify(s => s.AddAccount(It.Is<Account>(a => a.UserName == username && a.Password == password && a.Role == role)), Times.Once);
        }

        [Fact]
        public void Register_ShouldNotAddAccount_WhenUsernameAlreadyExists()
        {
            // Arrange
            var username = "existinguser";
            var password = "password";
            var role = "User";
            var existingAccount = new Account { AccountId = Guid.NewGuid(), UserName = username, Password = password, Role = role };
            _mockAccountService.Setup(s => s.GetAccountByUsername(username)).Returns(existingAccount);

            // Act
            _accountController.Register(username, password, role);

            // Assert
            _mockAccountService.Verify(s => s.AddAccount(It.IsAny<Account>()), Times.Never);
        }

        [Fact]
        public void Login_ShouldSetLoggedInAccount_WhenCredentialsAreValid()
        {
            // Arrange
            var username = "validuser";
            var password = "password";
            var account = new Account { AccountId = Guid.NewGuid(), UserName = username, Password = password, Role = "User" };
            _mockAccountService.Setup(s => s.GetAccountByUsername(username)).Returns(account);

            // Act
            _accountController.Login(username, password);

            // Assert
            Assert.True(_accountController.IsLoggedIn);
            Assert.Equal(account, _accountController.LoggedInAccount);
        }

        [Fact]
        public void Login_ShouldNotSetLoggedInAccount_WhenCredentialsAreInvalid()
        {
            // Arrange
            var username = "invaliduser";
            var password = "wrongpassword";
            var account = new Account { AccountId = Guid.NewGuid(), UserName = username, Password = "password", Role = "User" };
            _mockAccountService.Setup(s => s.GetAccountByUsername(username)).Returns(account);

            // Act
            _accountController.Login(username, password);

            // Assert
            Assert.False(_accountController.IsLoggedIn);
            Assert.Null(_accountController.LoggedInAccount);
        }

        [Fact]
        public void CreateAccount_ShouldCreateAccount_WhenLoggedInAsAdmin()
        {
            // Arrange
            var adminAccount = new Account { AccountId = Guid.NewGuid(), UserName = "admin", Password = "password", Role = "Admin" };
            _accountController.Login(adminAccount.UserName, adminAccount.Password);
            _accountController.LoggedInAccount = adminAccount;

            var username = "newuser";
            var password = "password";
            var role = "User";
            _mockAccountService.Setup(s => s.GetAccountByUsername(username)).Returns((Account)null);

            // Act
            _accountController.CreateAccount(username, password, role);

            // Assert
            _mockAccountService.Verify(s => s.AddAccount(It.Is<Account>(a => a.UserName == username && a.Password == password && a.Role == role)), Times.Once);
        }

        [Fact]
        public void CreateAccount_ShouldNotCreateAccount_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var userAccount = new Account { AccountId = Guid.NewGuid(), UserName = "user", Password = "password", Role = "User" };
            _accountController.Login(userAccount.UserName, userAccount.Password);
            _accountController.LoggedInAccount = userAccount;

            var username = "newuser";
            var password = "password";
            var role = "User";

            // Act
            _accountController.CreateAccount(username, password, role);

            // Assert
            _mockAccountService.Verify(s => s.AddAccount(It.IsAny<Account>()), Times.Never);
        }

        [Fact]
        public void UpdateAccount_ShouldUpdateAccount_WhenLoggedInAsAdmin()
        {
            // Arrange
            var adminAccount = new Account { AccountId = Guid.NewGuid(), UserName = "admin", Password = "password", Role = "Admin" };
            _accountController.Login(adminAccount.UserName, adminAccount.Password);
            _accountController.LoggedInAccount = adminAccount;

            var updatedAccount = new Account { AccountId = Guid.NewGuid(), UserName = "updateduser", Password = "newpassword", Role = "User" };

            // Act
            _accountController.UpdateAccount(updatedAccount);

            // Assert
            _mockAccountService.Verify(s => s.UpdateAccount(It.Is<Account>(a => a.AccountId == updatedAccount.AccountId)), Times.Once);
        }

        [Fact]
        public void UpdateAccount_ShouldNotUpdateAccount_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var userAccount = new Account { AccountId = Guid.NewGuid(), UserName = "user", Password = "password", Role = "User" };
            _accountController.Login(userAccount.UserName, userAccount.Password);
            _accountController.LoggedInAccount = userAccount;

            var updatedAccount = new Account { AccountId = Guid.NewGuid(), UserName = "updateduser", Password = "newpassword", Role = "User" };

            // Act
            _accountController.UpdateAccount(updatedAccount);

            // Assert
            _mockAccountService.Verify(s => s.UpdateAccount(It.IsAny<Account>()), Times.Never);
        }

        [Fact]
        public void RemoveAccountById_ShouldRemoveAccount_WhenLoggedInAsAdmin()
        {
            // Arrange
            var adminAccount = new Account { AccountId = Guid.NewGuid(), UserName = "admin", Password = "password", Role = "Admin" };
            _accountController.Login(adminAccount.UserName, adminAccount.Password);
            _accountController.LoggedInAccount = adminAccount;

            var accountIdToRemove = Guid.NewGuid();

            // Act
            _accountController.RemoveAccountById(accountIdToRemove);

            // Assert
            _mockAccountService.Verify(s => s.RemoveAccountById(accountIdToRemove), Times.Once);
        }

        [Fact]
        public void RemoveAccountById_ShouldNotRemoveAccount_WhenNotLoggedInAsAdmin()
        {
            // Arrange
            var userAccount = new Account { AccountId = Guid.NewGuid(), UserName = "user", Password = "password", Role = "User" };
            _accountController.Login(userAccount.UserName, userAccount.Password);
            _accountController.LoggedInAccount = userAccount;

            var accountIdToRemove = Guid.NewGuid();

            // Act
            _accountController.RemoveAccountById(accountIdToRemove);

            // Assert
            _mockAccountService.Verify(s => s.RemoveAccountById(It.IsAny<Guid>()), Times.Never);
        }
    }
}
