using System;
using Banks.Accounts;
using Banks.Classes;

namespace Banks.Service
{
    public class AccountFactory
    {
        private int _id;
        public Account CreateAccount(Bank bank, Client client, decimal limit)
        {
            Account account = new CreditAccount(_id++, decimal.Zero, client, limit);
            bank.AccountRepository.RegisterAccount(account);
            return account;
        }

        public Account CreateAccount(Bank bank, Client client)
        {
            Account account = new DebitAccount(_id++, decimal.Zero, client);
            bank.AccountRepository.RegisterAccount(account);
            return account;
        }

        public Account CreateAccount(Bank bank, Client client, DateTime validity)
        {
            Account account = new DepositAccount(_id++, decimal.Zero, client, validity);
            bank.AccountRepository.RegisterAccount(account);
            return account;
        }
    }
}