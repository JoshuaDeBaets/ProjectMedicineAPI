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

    [HttpGet( "GetProfile" )]
    public ActionResult<User> GetProfile(string token)
    {
        try
        {

            User u = _userManager.GetProfile ( token );
            if (u.Firstname == null)
            {
                return NotFound ("User does not exist") ;
            }
            return Ok(u);
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during the operation.
            //logger.LogError(ex, "An error occurred while retrieving the user's profile.");
            return StatusCode ( 500, ex.Message ); // Return a 500 error status.
        }
    }

    [HttpPost( "Login" )]
    public ActionResult<LoginResponse> Login( string email, string password )
    {
        try
        {
            //logger.LogInformation ( "Called Login with REST" );
            LoginResponse loginResponse = _userManager.Login ( email, password );
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
            return StatusCode(500, ex.Message );
        }
    }

    [HttpPost("Register" )]
    public ActionResult<LoginResponse> Register( string firstname, string lastname, string email, string password, string confirmPassword )
    {
        try
        {
            //logger.LogInformation ( "Called Register with REST" );
            LoginResponse loginResponse = _userManager.Register ( firstname, lastname, email, password, confirmPassword );
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
}