using System.Security.Cryptography;
using System;

namespace BL_Medicine.Tools;

public static class Encryptor
{
    public static string EncryptString( this string input )
    {
        try
        {
            using (MemoryStream memoryStream = new ( ))
            {
                using (Aes aes = Aes.Create ( ))
                {
                    byte[] key =
                    {
                        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                    };
                    aes.Key = key;

                    byte[] iv = aes.IV;
                    memoryStream.Write ( iv, 0, iv.Length );

                    using (CryptoStream cryptoStream = new (
                        memoryStream,
                        aes.CreateEncryptor ( ),
                        CryptoStreamMode.Write ))
                    {
                        using (StreamWriter encryptWriter = new ( cryptoStream ))
                        {
                            encryptWriter.WriteLine ( input );
                        }
                    }
                }

                // To get the encrypted string from the MemoryStream
                string encryptedString = Convert.ToBase64String ( memoryStream.ToArray ( ) );
                return encryptedString;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine ( $"Encryption failed: {ex}" );
            return null; // Return null in case of an error
        }
    }


    public static string DecryptString( this string encryptedString )
    {
        try
        {
            using (MemoryStream memoryStream = new ( Convert.FromBase64String ( encryptedString ) ))
            {
                using (Aes aes = Aes.Create ( ))
                {
                    byte[] key =
                    {
                        0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                        0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16
                    };
                    aes.Key = key;

                    byte[] iv = new byte[16];
                    memoryStream.Read ( iv, 0, iv.Length );
                    aes.IV = iv;

                    using (CryptoStream cryptoStream = new (
                        memoryStream,
                        aes.CreateDecryptor ( ),
                        CryptoStreamMode.Read ))
                    {
                        using (StreamReader decryptReader = new ( cryptoStream ))
                        {
                            return decryptReader.ReadLine ( );
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine ( $"Decryption failed: {ex}" );
            return null; // Return null in case of an error
        }
    }
}
