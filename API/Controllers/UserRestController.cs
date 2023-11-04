using System.Collections.Immutable;
using API.Interfaces;
using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.Managers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers;

[Route( "api/User" )]
[ApiController]
public class UserRestController : ControllerBase, IUserRestController
{
    private readonly UserManager _userManager;
    private readonly ILogger? _logger;

    public UserRestController( UserManager userManager, ILoggerFactory loggerFactory )
    {
        _userManager = userManager;
        //this.logger = loggerFactory.AddFile("logs/REST/UserRestControllerLog.txt").CreateLogger("UserRestController");
    }

    [HttpPost ( "Register" )]
    public ActionResult<LoginResponse> Register( string firstname, string lastname, string email, string password, string confirmPassword )
    {
        try
        {
            //logger.LogInformation ( "Called Register with REST" );
            var loginResponse = _userManager.Register ( firstname, lastname, email, password, confirmPassword );
            if (loginResponse == null)
            {
                return NotFound ( );
            }
            if (loginResponse.Token == null || loginResponse.HasError == true)
            {
                return Unauthorized ( loginResponse );
            }
            return Ok ( loginResponse );
        }
        catch (Exception ex)
        {
            return StatusCode ( 500, ex.Message );
        }
    }


    [HttpPost ( "Login" )]
    public ActionResult<LoginResponse> Login( string email, string password )
    {
        try
        {
            //logger.LogInformation ( "Called Login with REST" );
            var loginResponse = _userManager.Login ( email, password );
            if (loginResponse == null)
            {
                return NotFound ( );
            }
            if (loginResponse.Token == null || loginResponse.HasError == true)
            {
                return Unauthorized ( loginResponse );
            }
            return Ok ( loginResponse );
        }
        catch (Exception ex)
        {
            return StatusCode ( 500, ex.Message );
        }
    }


    [HttpGet ( "GetProfile" )]
    public ActionResult<User> GetProfileWithToken( string token )
    {
        try
        {

            User u = _userManager.GetProfileWithToken ( token );
            if (u.Firstname == null)
            {
                return NotFound ( "User does not exist" );
            }
            return Ok ( u );
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during the operation.
            //logger.LogError(ex, "An error occurred while retrieving the user's profile.");
            return StatusCode ( 500, ex.Message ); // Return a 500 error status.
        }
    }


    [HttpPut( "UpdateUser" )]
    public ActionResult<ErrorModel> UpdateUser(string token, string firstname, string surname, int weight, int height)
    {
        //TODO: Implement Gender
        try
        {
            var errorModel = _userManager.UpdateUser ( token, firstname, surname, weight, height );
            if (errorModel.HasError == true)
            {
                return BadRequest ( errorModel );
            }
            return Ok ( errorModel );
        }
        catch (Exception ex)
        {
            return StatusCode ( 500, ex.Message );
        }
    }

    [HttpPut( "UpdatePassword" )]
    public ActionResult<ErrorModel> UpdatePassword(string token, string newPassword, string confirmPassword)
    {
        try
        {
            var errorModel = new ErrorModel { ErrorMessage = "Not Implemented yet KEKW", HasError = true };
            if (errorModel.HasError == true)
            {
                return BadRequest ( errorModel );
            }
            return Ok ( errorModel );
        }
        catch (Exception ex)
        {
            return StatusCode ( 500, ex.Message );
        }
    }

    [HttpPut ( "UpdateEmail" )]
    public ActionResult<ErrorModel> UpdateEmail( string token, string newEmail )
    {
        try
        {
            var errorModel = new ErrorModel { ErrorMessage = "Not Implemented yet KEKW", HasError = true };
            if (errorModel.HasError == true)
            {
                return BadRequest ( errorModel );
            }
            return Ok ( errorModel );
        }
        catch (Exception ex)
        {
            return StatusCode ( 500, ex.Message );
        }
    }

    [HttpDelete ( "DeleteUser" )]
    public ActionResult<ErrorModel> DeleteUser( string token )
    {
        try
        {
            var errorModel = _userManager.DeleteUser ( token );
            if (errorModel.HasError == true)
            {
                return BadRequest ( errorModel );
            }
            return Ok ( errorModel );
        }
        catch (Exception ex)
        {
            return StatusCode ( 500, ex.Message );
        }
    }
}