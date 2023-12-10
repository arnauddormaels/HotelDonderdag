using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    public class PriceInfoRepositoryException : Exception
    {
        public PriceInfoRepositoryException(string? message) : base(message)
        {
        }

        public PriceInfoRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
