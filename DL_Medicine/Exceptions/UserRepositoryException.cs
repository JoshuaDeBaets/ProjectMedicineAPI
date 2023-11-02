using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DL_Medicine.Exceptions
{
    public class UserRepositoryException: Exception
    {
        public UserRepositoryException()
        {
        }

        public UserRepositoryException( string? message ) : base ( message )
        {
        }

        public UserRepositoryException( string? message, Exception? innerException ) : base ( message, innerException )
        {
        }

        protected UserRepositoryException( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }
    }
}
