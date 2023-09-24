// See https://aka.ms/new-console-template for more information

using BL_Medicine.Builders;
using BL_Medicine.Domain;

Console.WriteLine("Hello, World!");

User xenia = UserBuild();

static User UserBuild()
{
    var user = new UserBuilder()
        .SetFirstname( "Xenia" )
        .SetSurname( "hey" )
        .SetEmail( "hey" )
        .SetWeight( 256 )
        .SetHeight( 186 );

    return user.Build();
}
