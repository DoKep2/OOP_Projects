using Banks.Accounts;
using Banks.Classes;

namespace Banks.Chain.Requests
{
    public class WithDrawRequest : Request
    {
        public WithDrawRequest(Bank bank, int clientId, int accountId, int cashToDraw)
        {
            Bank = bank;
            ClientId = clientId;
            AccountId = accountId;
            CashToDraw = cashToDraw;
        }

        public Bank Bank { get; }
        public int ClientId { get; }
        public int AccountId { get; }
        public int CashToDraw { get; }
        public override void Execute()
        {
            Account account = Bank.AccountRepository.GetAccount(AccountId);
            Bank.AccountRepository.DeleteAccount(account);
            Bank.AccountRepository.RegisterAccount(account.ToBuilder().WithCash(account.Cash + CashToDraw).Build());
        }
    }
}