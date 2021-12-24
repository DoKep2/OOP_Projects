using System;
using Banks.Accounts;
using Banks.Chain.Requests;
using Banks.Classes;
using Banks.Exceptions;
using Banks.Service;

namespace Banks.Chain.Handlers
{
    public class ValidationHandler : AbstractHandler
    {
        public override object Handle(Request request)
        {
            switch (request)
            {
                case TopUpRequest upRequest:
                {
                    Account account = upRequest.Bank.AccountRepository.GetAccount(upRequest.AccountId);
                    Client client = CentralBank.ClientRepository.GetClient(upRequest.ClientId);
                    if (client.PassportId != account.Client.PassportId)
                    {
                        throw new BanksException("Invalid Client or Account id");
                    }

                    break;
                }

                case WithDrawRequest upRequest:
                {
                    Client client = CentralBank.ClientRepository.GetClient(upRequest.ClientId);
                    Account account = upRequest.Bank.AccountRepository.GetAccount(upRequest.AccountId);
                    if (account is DepositAccount depositAccount && DateTime.Now < depositAccount.Validity)
                    {
                        throw new BanksException($"Can't to with draw until {depositAccount.Validity}");
                    }

                    if (client.PassportId != account.Client.PassportId)
                    {
                        throw new BanksException("Invalid Client or Account id");
                    }

                    if (account is DebitAccount debitAccount && account.Cash - upRequest.CashToDraw < 0)
                    {
                        throw new BanksException("Invalid with draw: can't have negative balance");
                    }

                    if (account is CreditAccount creditAccount
                        && account.Cash - upRequest.CashToDraw < upRequest.Bank.Conditions.CreditBalanceLimit)
                    {
                        throw new BanksException("Invalid with draw: can't have balance less than limit");
                    }

                    break;
                }

                case TransferRequest upRequest:
                {
                    Client invoker = CentralBank.ClientRepository.GetClient(upRequest.AccountInvokerId);
                    Client receiver = CentralBank.ClientRepository.GetClient(upRequest.AccountReceiverId);
                    Account invokerAccount =
                        upRequest.BankInvoker.AccountRepository.GetAccount(upRequest.AccountInvokerId);
                    Account receiverAccount =
                        upRequest.BankReceiver.AccountRepository.GetAccount(upRequest.AccountReceiverId);
                    if (invoker.PassportId != invokerAccount.Client.PassportId)
                    {
                        throw new BanksException("Invalid invoker or account id");
                    }

                    if (receiver.PassportId != receiverAccount.Client.PassportId)
                    {
                        throw new BanksException("Invalid receiver or account id");
                    }

                    if (invokerAccount is DepositAccount depositAccount)
                    {
                        throw new BanksException($"Can't to transfer until {depositAccount.Validity}");
                    }

                    break;
                }
            }

            return base.Handle(request);
        }
    }
}