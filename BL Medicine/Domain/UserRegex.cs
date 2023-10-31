using System.Text.RegularExpressions;

namespace BL_Medicine.RegexChecks;

public static class UserRegex
{
    public static bool IsValidName(this string input)
    {
        Regex regex = new ("^[a-zA-Z -]+$");
        return regex.IsMatch(input);
    }

    public static bool IsValidEmail(this string email )
    {
        Regex regex = new (@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
        return regex.IsMatch(email);
    }
}