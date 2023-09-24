using BL_Medicine.Builders;
using BL_Medicine.Domain;

namespace ConsoleTesting;

public class Program
{
    static void Main()
    {

        var builder = new UserBuilder()
            .SetFirstname( "Josh" )
            .SetSurname( "De Baets" )
            .SetEmail( "josh@gmail.com" )
            .SetDateOfBirth( new DateTime( 1994, 8, 7 ) );
        

        User user = builder.Build();
        Console.WriteLine(user.Firstname + " " + user.Surname);
    }
}