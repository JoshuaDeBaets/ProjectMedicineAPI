namespace BL_Medicine.Domain;

public class LoginResponse
{
    public string? Token { get; set; }
    public bool HasError { get; set; }
    
    public string? ErrorMessage { get; set; }
    public bool? IsAdmin { get; set; }

    public LoginResponse()
    { }

    public LoginResponse(string token, bool hasError, bool isAdmin)
    {
        this.Token = token;
        this.HasError = hasError;
        this.IsAdmin = isAdmin;
    }

    public override string ToString()
    {
        return "Token: " + Token + " HasError: " + HasError + " IsAdmin: " + IsAdmin;
    }
    
}