using Banks.Chain.Requests;

namespace Banks.Chain.Handlers
{
    public class TopUpHandler : AbstractHandler
    {
        public override object Handle(Request request)
        {
            if (request is TopUpRequest)
            {
                request.Execute();
                return null;
            }
            else
            {
                return base.Handle(request);
            }
        }
    }
}