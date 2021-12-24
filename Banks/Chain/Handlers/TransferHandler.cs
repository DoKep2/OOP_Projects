using Banks.Chain.Requests;

namespace Banks.Chain.Handlers
{
    public class TransferHandler : AbstractHandler
    {
        public override object Handle(Request request)
        {
            if (request is TransferRequest)
            {
                request.Execute();
            }

            return base.Handle(request);
        }
    }
}