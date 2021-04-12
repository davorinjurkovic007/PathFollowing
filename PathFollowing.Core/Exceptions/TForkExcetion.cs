  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFollowing.Core.Exceptions
{
    public class TForkExcetion : Exception
    {
        public TForkExcetion(string message) : base(message)
        {
        }

        public TForkExcetion(string message, string tForkIsFounded) : base(message)
        {
            TForkIsFounded = tForkIsFounded;
        }

        public string TForkIsFounded { get; private set; }
    }
}
