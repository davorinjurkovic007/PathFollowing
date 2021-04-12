using System;

namespace PathFollowing.Data.Exceptions
{
    public class NoneEndPositionException : Exception
    {
        public NoneEndPositionException(string message) : base(message)
        {
        }

        public NoneEndPositionException(string message, string noneEndPosition) : base(message)
        {
            NoneEndPosition = noneEndPosition;
        }

        public string NoneEndPosition { get; private set; }
    }
}
