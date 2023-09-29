namespace BL_Medicine.RegexChecks;

public static class Extensions
{
    public static bool isNull( this string s)
    {
        if (string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s))
        {
            return true;
        }
        return false;
    }
}