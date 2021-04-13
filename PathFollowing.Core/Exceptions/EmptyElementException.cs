using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFollowing.Core.Exceptions
{
    public class EmptyElementException : Exception
    {
        public EmptyElementException(string message) : base(message)
        {
        }

        public EmptyElementException(string message, string emptyElement) : base(message)
        {
            EmptyElement = emptyElement;
        }

        public string EmptyElement { get; private set; }
    }
}
