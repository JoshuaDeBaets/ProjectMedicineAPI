using System.Net.Cache;
using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.Managers;
using BL_Medicine.RegexChecks;
using BL_Medicine.Repositories;
using DL_Medicine;
using System.Security.Cryptography;

namespace ConsoleTesting;

public abstract class Program
{
    static void Main()
    {
        string stringtoencrypt = "RobbeIsGeil";

        string encrypted = stringtoencrypt.EncryptString ( );

        string decrypted = encrypted.DecryptString ( );

        

        Console.WriteLine ( Encryptor.EncryptString ( stringtoencrypt ) );

        Console.WriteLine (Encryptor.DecryptString ( encrypted ) );
    }
}