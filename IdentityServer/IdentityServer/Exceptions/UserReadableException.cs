using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Exceptions
{
    [Serializable]
    public class UserReadableException : Exception
    {
        public UserReadableException() { }

        public UserReadableException(string message)
            : base(message) { }

        public UserReadableException(string message, Exception inner)
            : base(message, inner) { }
    }
}
