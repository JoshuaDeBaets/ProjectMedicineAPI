// See https://aka.ms/new-console-template for more information

using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.RegexChecks;

Console.WriteLine("Hello, World!");

User xenia = UserBuild();

static User UserBuild()
{
    string stringtoencrypt = "HelloThere";

    Console.WriteLine ( Encryptor.EncryptString ( stringtoencrypt ) );
}
