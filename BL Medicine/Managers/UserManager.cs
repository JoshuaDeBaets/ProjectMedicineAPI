using System.Runtime.InteropServices;
using System.Runtime.InteropServices.JavaScript;
using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.Exceptions;
using BL_Medicine.Tools;
using BL_Medicine.Repositories;

namespace BL_Medicine.Managers;

public class UserManager
{
    private IUserRepository _userRepository;

    public UserManager(IUserRepository userRepository)
    {
        _userRepository = userRepository;
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

    public LoginResponse Register(string firstname, string lastname, string email, string password, string confirmPassword)
    {
        try
        {
            if ( firstname.isNull() || lastname.isNull() || email.isNull() || password.isNull() ||
                 confirmPassword.isNull() )
            {
                var response = new LoginResponse()
                {
                    HasError = true,
                    ErrorMessage = "One or more of the fields are null"
                };
                return response;
            }

            if (password != confirmPassword)
            {
                var response = new LoginResponse ( )
                {
                    HasError = true,
                    ErrorMessage = "Password and confirmPassword do not match"
                };
                return response;
            }

            return _userRepository.Register ( firstname, lastname, email, password, confirmPassword );
            
        }
        catch ( Exception e )
        {
            throw new UserException( "Error Registering", e );
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

    public ErrorModel UpdateProfile(User user)
    {
        try
        {
            throw new NotImplementedException ( );
        }
        catch ( Exception e)
        {
            throw new UserException ( "Error Updating Profile", e );
        }
    }

    public ErrorModel DeleteProfile(User user)
    {
        try
        {
            throw new NotImplementedException ( );
        }
        catch (Exception e)
        {
            throw new Exception ( "Error Deleting Profile", e );
        }
    }
}