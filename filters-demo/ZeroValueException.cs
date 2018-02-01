using System;

namespace filters_demo
{
    public class ZeroValueException : Exception
    {
        public ZeroValueException(){}

        public ZeroValueException(string message)
        : base(message)
        { }

        public ZeroValueException(string message, Exception innerException)
        : base(message, innerException)
        { }
    }
}