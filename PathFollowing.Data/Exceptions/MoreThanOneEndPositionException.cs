using System;

namespace PathFollowing.Data.Exceptions
{
    public class MoreThanOneEndPositionException : Exception
    {
        public MoreThanOneEndPositionException(string message) : base(message)
        {
        }

        public MoreThanOneEndPositionException(string message, string moreThanOneEndPosition) : base(message)
        {
            MoreThanOneEndPosition = moreThanOneEndPosition;
        }

        public string MoreThanOneEndPosition { get; private set; }
    }
}
