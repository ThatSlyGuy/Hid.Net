using System;

namespace Hid.Net
{
    public class HidException : Exception
    {
        public HidException(string message) : base(message)
        {
        }
    }
}
