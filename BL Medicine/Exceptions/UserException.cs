namespace BL_Medicine.Exceptions;

public class UserException : Exception
{
    public string? ErrorCode;
    public DateTime? Timestamp;
    
    public UserException( string? message ) : base( message )
    {
        
    }

    public UserException( string? message, Exception? innerException ) : base( message, innerException )
    {
        
    }
    
    public UserException(string message, string errorCode, DateTime timestamp)
        : base(message)
    {
        ErrorCode = errorCode;
        Timestamp = timestamp;
    }

    public UserException(string message, string errorCode, DateTime timestamp, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        Timestamp = timestamp;
    }
}
