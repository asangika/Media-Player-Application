using MediaPlayer.Domain.src.UserAggregate;

namespace MediaPlayer.Service.src.Services
{
    public class AccountService
    {
        private readonly List<Account> _accounts = new List<Account>();

        public AccountService(List<Account> accounts)
        {
            this._accounts = accounts;
        }

        public Account GetAccountByUsername(string username) => _accounts.FirstOrDefault(a => a.UserName == username);

        public Account GetAccountById(Guid id)
        {
            return _accounts.FirstOrDefault(a => a.AccountId == id);
        }
        public void AddAccount(Account account)
        {
            if (_accounts.Any(a => a.UserName == account.UserName))
            {
                throw new Exception("An account with the same username already exists.");
            }
            _accounts.Add(account);
        }

        public void UpdateAccount(Account updatedAccount)
        {
            var account = GetAccountById(updatedAccount.AccountId);
            if (account != null)
            {
                account.UserName = updatedAccount.UserName;
                account.Password = updatedAccount.Password;
                account.Role = updatedAccount.Role;
            }
        }

        public void RemoveAccountById(Guid id)
        {
            var account = _accounts.FirstOrDefault(a => a.AccountId == id);
            if (account != null) _accounts.Remove(account);
        }

        public List<Account> GetAllAccounts() => _accounts;
    }
}