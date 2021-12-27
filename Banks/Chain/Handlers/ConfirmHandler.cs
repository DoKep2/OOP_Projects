using System;
using Banks.Chain.Requests;

namespace Banks.Chain.Handlers
{
    public class ConfirmHandler : AbstractHandler
    {
        public override object Handle(Request request)
        {
            Console.WriteLine("Type CONFIRM to continue");
            string confirm = Console.ReadLine();
            return confirm != "CONFIRM" ? null : base.Handle(request);
        }
    }
}