using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class DescriptionManagerException : Exception
    {
        public DescriptionManagerException(string? message) : base(message)
        {
        }

        public DescriptionManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
    
}
