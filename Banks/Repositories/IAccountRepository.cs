using System.Collections.Generic;
using Banks.Accounts;

namespace Banks.Repositories
{
    public interface IAccountRepository
    {
        Account RegisterAccount(Account account);
        List<Account> GetAll();
        Account GetAccount(int id);
        void DeleteAccount(Account account);
    }
}