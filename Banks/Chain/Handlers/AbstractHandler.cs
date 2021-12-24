using Banks.Chain.Requests;

namespace Banks.Chain.Handlers
{
    public abstract class AbstractHandler : IHandler
    {
        private IHandler _nextHandler;
        public IHandler SetNext(IHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual object Handle(Request request)
        {
            return _nextHandler?.Handle(request);
        }
    }
}