using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Exceptions
{
    public class OrganisorRepositoryException : Exception
    {
        public OrganisorRepositoryException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected OrganisorRepositoryException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
