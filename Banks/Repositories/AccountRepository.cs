using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Exceptions;

namespace Banks.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly List<Account> _accounts;

        public AccountRepository()
        {
            _accounts = new List<Account>();
        }

        public Account RegisterAccount(Account account)
        {
            _accounts.Add(account);
            return account;
        }

        public void DeleteAccount(Account account)
        {
            _accounts.Remove(account);
        }

        public List<Account> GetAll()
        {
            return new List<Account>(_accounts);
        }

        public Account GetAccount(int id)
        {
            Account account = _accounts.FirstOrDefault(currentAccount => currentAccount.Id == id);
            if (account == null)
            {
                throw new BanksException("Invalid account id");
            }

            return account;
        }
    }
}