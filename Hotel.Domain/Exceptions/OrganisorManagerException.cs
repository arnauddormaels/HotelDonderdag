using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class OrganisorManagerException : Exception
    {
        public OrganisorManagerException(string? message) : base(message)
        {
        }

        public OrganisorManagerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    
    
    }
}
