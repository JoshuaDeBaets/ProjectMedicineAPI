using System.Security.Cryptography;

namespace BL_Medicine.Tools;

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

    public static string GenerateRandomSecretKey( int keyLengthInBytes )
    {
        if (keyLengthInBytes <= 0 || keyLengthInBytes % 2 != 0)
        {
            throw new ArgumentException ( "Key length must be a positive even number of bytes." );
        }

        byte[] keyBytes = new byte[keyLengthInBytes];

        using (var rng = RandomNumberGenerator.Create ( ))
        {
            rng.GetBytes ( keyBytes );
        }

        return Convert.ToBase64String ( keyBytes );
    }

    public static string Add( this string s, string texttoadd)
    {
        return s + texttoadd;
    }
}