using Banks.Accounts;
using Banks.Classes;

namespace Banks.Chain.Requests
{
    public class TopUpRequest : Request
    {
        public TopUpRequest(Bank bank, int clientId, int accountId, int cashToTopUp)
        {
            Bank = bank;
            ClientId = clientId;
            AccountId = accountId;
            CashToTopUp = cashToTopUp;
        }

        public Bank Bank { get; }
        public int ClientId { get; }
        public int AccountId { get; }
        private int CashToTopUp { get; }
        public override void Execute()
        {
            Account account = Bank.AccountRepository.GetAccount(AccountId);
            Bank.AccountRepository.DeleteAccount(account);
            Bank.AccountRepository.RegisterAccount(account.ToBuilder().WithCash(account.Cash + CashToTopUp).Build());
        }
    }
}