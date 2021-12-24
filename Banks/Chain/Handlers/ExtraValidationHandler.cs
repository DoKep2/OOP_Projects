using System;
using Banks.Chain.Requests;
using Banks.Exceptions;

namespace Banks.Chain.Handlers
{
    public class ExtraValidationHandler : AbstractHandler
    {
        public override object Handle(Request request)
        {
            switch (request)
            {
                case WithDrawRequest upRequest:
                {
                    if (upRequest.CashToDraw > upRequest.Bank.ExtraConditions.OperationLimit)
                    {
                        throw new BanksException("Limit for with draw is exceeded");
                    }

                    break;
                }

                case TransferRequest upRequest:
                {
                    if (upRequest.CashToTransfer > upRequest.BankInvoker.ExtraConditions.OperationLimit)
                    {
                        throw new BanksException("Limit for transfer is exceeded");
                    }

                    break;
                }
            }

            return base.Handle(request);
        }
    }
}