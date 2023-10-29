using System.Collections.Immutable;
using API.Interfaces;
using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.Managers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route( "api/[controller]" )]
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
            
            
            var b = new UserBuilder()
                .SetFirstname( "Hodor" )
                .SetSurname( "Jansenss" )
                .SetEmail( "jansenss@gmail.com" )
                .Password( "Idk" );

            User u = b.Build();
            
            return Ok(u);
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during the operation.
            //logger.LogError(ex, "An error occurred while retrieving the user's profile.");
            return StatusCode(500, "Internal server error"); // Return a 500 error status.
        }
    }
}