using System;

namespace PathFollowing.Data.Exceptions
{
    public class MoreThanOneStartPositionException : Exception
    {
        public MoreThanOneStartPositionException(string message) : base(message)
        {
        }

        public MoreThanOneStartPositionException(string message, string moreThanOneStartPosition) : base(message)
        {
            MoreThanOneStartPosition = moreThanOneStartPosition;
        }

        public string MoreThanOneStartPosition { get; private set; }
    }
}
