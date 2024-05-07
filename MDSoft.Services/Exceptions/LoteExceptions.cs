using System;

namespace PNComm.Common.Exceptions

{
    public class LoteExceptions : Exception
    {
        public LoteExceptions()
        {

        }

        public LoteExceptions(string message) :base(message)
        {

        }

        public LoteExceptions(string message, Exception inner) : base(message, inner)
        {

        }

    }
}
