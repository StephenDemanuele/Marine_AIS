using System;

namespace AIS.Parser.Exceptions
{
    public class ListenerAlreadyRunningException : Exception
    {
        public ListenerAlreadyRunningException()
            : base("Stop listener before starting again.")
        {
        }
    }
}
