using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenLibrary.Core.Exceptions
{
    public class HostException : Exception
    {
        public HostException() { }

        public HostException(string message) : base(message) { }

        public HostException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
