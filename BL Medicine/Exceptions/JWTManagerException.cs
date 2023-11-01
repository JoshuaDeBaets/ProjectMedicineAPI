using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BL_Medicine.Exceptions
{
    public class JWTManagerException: Exception
    {
        public JWTManagerException()
        {
        }

        public JWTManagerException( string? message ) : base ( message )
        {
        }

        public JWTManagerException( string? message, Exception? innerException ) : base ( message, innerException )
        {
        }

        protected JWTManagerException( SerializationInfo info, StreamingContext context ) : base ( info, context )
        {
        }
    }
}
