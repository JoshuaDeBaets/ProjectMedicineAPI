using BL_Medicine.Builders;
using BL_Medicine.Domain;

namespace ConsoleTesting;

public class Program
{
    static void Main()
    {

        UserBuilder builder = new ();
        builder.SetFirstname( "Josh" );
        builder.SetSurname( "De Baets" );
        builder.SetEmail("josh@gmail.com");

        User user = builder.Build();
        Console.WriteLine(user.Firstname + " " + user.Surname);
    }
}