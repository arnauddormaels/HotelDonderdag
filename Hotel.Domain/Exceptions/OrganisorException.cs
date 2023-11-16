using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class OrganisorException : Exception
    {
        public OrganisorException(string message) : base(message)
        {
        }

        public OrganisorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
