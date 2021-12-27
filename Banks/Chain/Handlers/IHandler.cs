using Banks.Chain.Requests;

namespace Banks.Chain.Handlers
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        object Handle(Request request);
    }
}