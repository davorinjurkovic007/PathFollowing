using System;

namespace PathFollowing.Data.Exceptions
{
    public class NoneStartPositionException : Exception
    {
        public NoneStartPositionException(string message) : base(message)
        {
        }

        public NoneStartPositionException(string message, string noneStartPosition) : base(message)
        {
            NoneStartPosition = noneStartPosition;
        }

        public string NoneStartPosition { get; private set; }
    }
}
