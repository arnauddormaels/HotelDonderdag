using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    public class DescriptionRepositoryException : Exception
    {
        public DescriptionRepositoryException(string? message) : base(message)
        {
        }

        public DescriptionRepositoryException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
