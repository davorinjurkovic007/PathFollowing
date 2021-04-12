using System;

namespace PathFollowing.Data.Exceptions
{
    public class FilePahtException : Exception
    {
        public FilePahtException(string message) : base(message)
        {
        }

        public FilePahtException(string message, string pathString) : base(message)
        {
            RequestedFilePath = pathString;
        }

        public string RequestedFilePath { get; private set; }
    }
}
