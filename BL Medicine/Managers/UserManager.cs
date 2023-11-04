using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.Exceptions;
using BL_Medicine.Tools;
using BL_Medicine.Repositories;
using System.Security.Claims;

namespace BL_Medicine.Managers;

public class UserManager
{
    private IUserRepository _userRepository;
    private string jwtSecret;

    public UserManager(IUserRepository userRepository )
    {
        _userRepository = userRepository;
        jwtSecret = @"c1+D+ixsmMqtoQADJyXMFMjsOCp1yErBB6WNLvN7sGfMOS2m20s6HSE0UP7KjkePMEM6/p/oP/c6Zod7TjT5ww==";
    }

    public LoginResponse Login(string email, string password)
    {
        try
        {
            if ( email.isNull() || password.isNull() )
            {
                var response = new LoginResponse ( )
                {
                    HasError = true,
                    ErrorMessage = "One or more of the fields are null",
                    Token = "",
                };
                return response;
            }

            return _userRepository.Login ( email, password );
        }
        catch ( Exception e )
        {
            throw;
        }
        
    }

    public LoginResponse Register( string firstname, string surname, string email, string password, string confirmPassword )
    {
        try
        {

            if (string.IsNullOrEmpty ( firstname ) || string.IsNullOrEmpty ( surname ) || string.IsNullOrEmpty ( email ) || string.IsNullOrEmpty ( password ) || string.IsNullOrEmpty ( confirmPassword ))
                return new LoginResponse { HasError = true, ErrorMessage = "One or more of the fields are null" };

            if (!firstname.IsValidName ( ) )
                return new LoginResponse { HasError = true, ErrorMessage = "Invalid firstname" };

            if (!surname.IsValidName ( ) )
                return new LoginResponse { HasError = true, ErrorMessage = "Invalid surname" };

            if (!email.IsValidEmail ( ) )
                return new LoginResponse { HasError = true, ErrorMessage = "Invalid emailadress" };

            if (password != confirmPassword)
                return new LoginResponse { HasError = true, ErrorMessage = "Password and confirmPassword do not match" };

            if (_userRepository.userExists ( email ))
                return new LoginResponse { HasError = true, ErrorMessage = "A User with this email already exists" };

            return _userRepository.Register ( firstname, surname, email, password, confirmPassword );
        }
        catch (Exception e)
        {
            throw new UserException ( "Error Registering", e );
        }
    }

    public User GetProfile(string email)
    {
        try
        {
            if (email.isNull ( ))
            {
                throw new UserException ( "Email is null" );
            }

            return _userRepository.GetProfile ( email );
        } catch (Exception e)
        {
            throw;
        }

    }

    public User GetProfileWithToken( string token )
    {
        var user = new User ( );
        string email = "";

        var jwtManager = new JWTManager ( jwtSecret );

        if (!jwtManager.IsTokenValid ( token ))
        {
            throw new UserException ( "Token is not valid" );
        }
        else
        {
            List<Claim> claims = jwtManager.GetTokenClaims ( token ).ToList ( );
            email = claims.FirstOrDefault ( c => c.Type.Equals ( ClaimTypes.Email ) ).Value;
            return _userRepository.GetProfile ( email );
        }
    }

    public ErrorModel UpdateUser( string token, string firstname, string surname, int weight, int height )
    {
        try
        {
            var jwtManager = new JWTManager ( jwtSecret );
            if (!jwtManager.IsTokenValid ( token ))
            {
                return new ErrorModel { HasError = true, ErrorMessage = "Token is not valid" };
            }

            List<Claim> claims = jwtManager.GetTokenClaims ( token ).ToList ( );
            string id = claims.FirstOrDefault ( c => c.Type.Equals ( ClaimTypes.NameIdentifier ) ).Value;
            return _userRepository.UpdateUser ( id, firstname, surname, weight, height );
        }
        catch ( Exception e)
        {
            throw new UserException ( "Error Updating Profile", e );
        }
    }

    public ErrorModel DeleteUser(string token)
    {
        
        try
        {
            string id = null;
            var jwtManager = new JWTManager ( jwtSecret );
            if (!jwtManager.IsTokenValid ( token ))
            {
                return new ErrorModel { HasError = true, ErrorMessage = "Token is not valid" };
            }

            List<Claim> claims = jwtManager.GetTokenClaims ( token ).ToList ( );
            id = claims.FirstOrDefault ( c => c.Type.Equals ( ClaimTypes.NameIdentifier ) ).Value;
            return _userRepository.DeleteUser(id);
            
        }
        catch (Exception e)
        {
            throw new Exception ( "Error Deleting Profile", e );
        }
    }
}