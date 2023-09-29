using System.Net.Cache;
using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.Managers;
using BL_Medicine.Repositories;
using DL_Medicine;

namespace ConsoleTesting;

public abstract class Program
{
    static void Main()
    {

        var builder = new UserBuilder()
            .SetFirstname( "Josh" )
            .SetSurname( "De Baets" )
            .SetEmail( "josh@gmail.com" )
            .SetDateOfBirth( new DateTime( 1994, 8, 7 ) )
            .SetWeight( 85 )
            .SetHeight( 187 );
            

        User user = builder.Build();
        Console.WriteLine( $"{user.Firstname} {user.Surname}" );
        Console.WriteLine( user.Height.ToString() );

        UserRepository repository = new UserRepository();
        UserManager userManager = new UserManager( repository );
        
        
        
        
    }
}