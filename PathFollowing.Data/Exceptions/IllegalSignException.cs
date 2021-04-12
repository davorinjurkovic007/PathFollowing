using System;

namespace PathFollowing.Data.Exceptions
{
    public class IllegalSignException : Exception
    {
        public IllegalSignException(string message) : base(message)
        {
        }

        public IllegalSignException(string message, string illegalSign) : base(message)
        {
            IllegalSign = illegalSign;
        }

        public string IllegalSign { get; private set; }
    }
}
