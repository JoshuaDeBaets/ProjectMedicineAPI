using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace BL_Medicine.RegexChecks;

public class Encryptor
{
    private readonly byte[] key;
    private readonly byte[] iv;

    public Encryptor(byte[] key, byte[] iv)
    {
        this.key = key ?? throw new ArgumentNullException(nameof(key));
        this.iv = iv ?? throw new ArgumentNullException(nameof(iv));

        // Ensure that the key and IV have the appropriate lengths.
        if (key.Length != 16 && key.Length != 24 && key.Length != 32)
        {
            throw new ArgumentException("Invalid key length. AES-128, AES-192, or AES-256 key lengths are supported.");
        }

        if (iv.Length != 16)
        {
            throw new ArgumentException("Invalid IV length. It should be 16 bytes for AES.");
        }
    }

    public string Encrypt(string plainText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                }

                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }

    public string Decrypt(string cipherText)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = key;
            aesAlg.IV = iv;

            ICryptoTransform decrypt = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decrypt, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }
}