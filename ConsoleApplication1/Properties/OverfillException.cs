
using System;

namespace ConsoleApplication1.Properties
{
    public class OverfillException : Exception
    {
        public OverfillException(string message) : base(message) { }
    }
}