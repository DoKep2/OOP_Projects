using Banks.Accounts;
using Banks.Classes;
using Banks.Service;

namespace Banks.Chain.Requests
{
    public class TransferRequest : Request
    {
        public TransferRequest(
            Bank bankInvoker,
            Bank bankReceiver,
            int clientInvokerId,
            int clientReceiverId,
            int accountInvokerId,
            int accountReceiverId,
            int cashToTransfer)
        {
            BankInvoker = bankInvoker;
            BankReceiver = bankReceiver;
            ClientInvokerId = clientInvokerId;
            ClientReceiverId = clientReceiverId;
            AccountInvokerId = accountInvokerId;
            AccountReceiverId = accountReceiverId;
            CashToTransfer = cashToTransfer;
        }

        public Bank BankInvoker { get; }
        public Bank BankReceiver { get; }
        public int ClientInvokerId { get; }
        public int ClientReceiverId { get; }
        public int AccountInvokerId { get; }
        public int AccountReceiverId { get; }
        public int CashToTransfer { get; }
        public override void Execute()
        {
            Account accountInvoker = BankReceiver.AccountRepository.GetAccount(AccountInvokerId);
            Account accountReceiver = BankReceiver.AccountRepository.GetAccount(AccountReceiverId);
            accountReceiver = new Account.AccountBuilder().WithCash(accountReceiver.Cash + CashToTransfer).Build();
            accountInvoker = new Account.AccountBuilder().WithCash(accountInvoker.Cash - CashToTransfer).Build();
        }
    }
}