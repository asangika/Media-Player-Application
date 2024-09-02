using MediaPlayer.Domain.src.UserAggregate;
using MediaPlayer.Service.src.Services;

namespace MediaPlayer.Controller.src
{
    public class AccountController
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        public bool IsLoggedIn { get; private set; }
        public Account LoggedInAccount { get; set; }

        public void Register(string username, string password, string role)
        {
            if (_accountService.GetAccountByUsername(username) == null)
            {
                var account = new Account { AccountId = Guid.NewGuid(), UserName = username, Password = password, Role = role };
                _accountService.AddAccount(account);
            }
        }

        public void Login(string username, string password)
        {
            var account = _accountService.GetAccountByUsername(username);
            if (account != null && account.Password == password)
            {
                IsLoggedIn = true;
                LoggedInAccount = account;
            }
        }

        public void CreateAccount(string username, string password, string role)
        {
            if (IsLoggedIn && LoggedInAccount != null && LoggedInAccount.Role == "Admin")
            {
                Register(username, password, role);
                Console.WriteLine($"User '{username}' created successfully.");
            }
            else
            {
                Console.WriteLine("Only admins can create new accounts.");
            }
        }

        public Account GetAccountById(Guid id)
        {
            return _accountService.GetAccountById(id);
        }

        public void UpdateAccount(Account updatedAccount)
        {
            if (IsLoggedIn && LoggedInAccount != null && LoggedInAccount.Role == "Admin")
            {
                _accountService.UpdateAccount(updatedAccount);
                Console.WriteLine($"User '{updatedAccount.UserName}' updated successfully.");
            }

            else
            {
                Console.WriteLine("Only admins can update accounts.");
            }
        }

        public void RemoveAccountById(Guid id)
        {
            if (IsLoggedIn && LoggedInAccount != null && LoggedInAccount.Role == "Admin")
            {
                _accountService.RemoveAccountById(id);
                Console.WriteLine($"User with id '{id}' removed successfully.");
            }

            else
            {
                Console.WriteLine("Only admins can remove accounts.");
            }
        }
    }
}