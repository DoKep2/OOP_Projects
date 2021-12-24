using Banks.Accounts;
using Banks.Classes;
using Banks.Repositories;

namespace Banks.Service
{
    public static class CentralBank
    {
        public static IClientRepository ClientRepository { get; } = new ClientRepository();
        public static IBankRepository BankRepository { get; } = new BankRepository();
        public static void Recount()
        {
            foreach (Bank bank in BankRepository.GetAll())
            {
                foreach (Account account in bank.AccountRepository.GetAll())
                {
                    account.ApplyConditions(bank.Conditions);
                }
            }
        }
    }
}