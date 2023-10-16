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


        string hi = "Nosferatu21";
        byte[] Key = KeyAndIvGenerator.GenerateRandomKey(128);

        // Generate a 128-bit (16-byte) IV for AES encryption.
        byte[] IV = KeyAndIvGenerator.GenerateRandomIv(128);
        Encryptor crypto = new( Key, IV );
        string cript = crypto.Encrypt( hi );

        Console.Write(cript);


    }
}