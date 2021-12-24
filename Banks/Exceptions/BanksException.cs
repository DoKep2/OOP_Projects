using System;

namespace Banks.Exceptions
{
    public class BanksException : Exception
    {
        public BanksException(string message)
            : base(message)
        {
        }
    }
}