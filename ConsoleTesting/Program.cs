using System.Net.Cache;
using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.Managers;
using BL_Medicine.Tools;
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

        string encrypted2 = stringtoencrypt.EncryptString ( );

        string encrypted3 = stringtoencrypt.EncryptString ( );



        string test1 = encrypted;

        string test2 = encrypted;

        string decrypted = encrypted.DecryptString ( );

        string decrypted2 = encrypted2.DecryptString ( );

        string decrypted3 = encrypted3.DecryptString ( );

        Console.WriteLine ( encrypted );
        Console.WriteLine(encrypted2); Console.WriteLine(encrypted3);

        Console.WriteLine ( decrypted );
        Console.WriteLine ( decrypted2 );
        Console.WriteLine ( decrypted3 );


        Console.WriteLine ( Extensions.GenerateRandomSecretKey( 64 ) );
            ;

    }
}