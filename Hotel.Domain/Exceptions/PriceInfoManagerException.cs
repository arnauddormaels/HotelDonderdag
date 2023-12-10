using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Exceptions
{
    public class PriceInfoManagerException : Exception
    {
        public PriceInfoManagerException(string? message) : base(message)
        {
        }

        public PriceInfoManagerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
