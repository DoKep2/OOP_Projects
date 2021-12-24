using Banks.Chain.Requests;

namespace Banks.Chain.Handlers
{
    public class WithDrawHandler : AbstractHandler
    {
        public override object Handle(Request request)
        {
            if (request is WithDrawRequest)
            {
                request.Execute();
            }

            return base.Handle(request);
        }
    }
}