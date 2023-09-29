namespace BL_Medicine.Domain;

public  class ErrorModel
{
    public int ErrorCode;
    public bool HasError;
    public string ErrorMessage;

    public ErrorModel(int errorCode, string errorMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = errorMessage;
    }
    
    
}