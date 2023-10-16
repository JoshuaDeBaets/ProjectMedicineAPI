namespace BL_Medicine.RegexChecks;

using System.Security.Cryptography;

using System;
using System.Security.Cryptography;

public class KeyAndIvGenerator
{
    public static byte[] GenerateRandomKey(int keySizeInBytes)
    {
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] key = new byte[keySizeInBytes];
            rng.GetBytes(key);
            return key;
        }
    }

    public static byte[] GenerateRandomIv(int ivSizeInBytes)
    {
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            byte[] iv = new byte[ivSizeInBytes];
            rng.GetBytes(iv);
            return iv;
        }
    }
}
