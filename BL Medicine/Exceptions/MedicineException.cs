namespace BL_Medicine.Exceptions;

public class MedicineException : Exception
{
    public string? ErrorCode;
    public DateTime? Timestamp;
    
    public MedicineException( string? message ) : base( message )
    {
        
    }

    public MedicineException( string? message, Exception? innerException ) : base( message, innerException )
    {
        
    }
    
    public MedicineException(string message, string errorCode, DateTime timestamp)
        : base(message)
    {
        ErrorCode = errorCode;
        Timestamp = timestamp;
    }

    public MedicineException(string message, string errorCode, DateTime timestamp, Exception innerException)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
        Timestamp = timestamp;
    }
}