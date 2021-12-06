using System;

namespace Backups.Exceptions
{
    public class BackupsException : Exception
    {
        public BackupsException(string message)
            : base(message) { }
    }
}