using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    internal class EventRepositoryException : Exception
    {
        public EventRepositoryException(string? message) : base(message)
        {
        }

        public EventRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
