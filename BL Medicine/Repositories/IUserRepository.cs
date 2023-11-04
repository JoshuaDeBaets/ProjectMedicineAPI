using BL_Medicine.Domain;

namespace BL_Medicine.Repositories;

public interface IUserRepository
{
    LoginResponse Login(string email, string password);
    LoginResponse Register( string firstname, string lastname, string email, string password, string confirmPassword );

    User GetProfile( string email );
    bool userExists( string email );
    ErrorModel UpdateUser( string id, string? firstname, string? surname, int? weight, int? height );
    ErrorModel DeleteUser( string id );
}