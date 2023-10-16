using System.Collections.Immutable;
using BL_Medicine.Builders;
using BL_Medicine.Domain;
using BL_Medicine.Managers;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route( "api/[controller]" )]
[ApiController]
public class UserRestController : ControllerBase
{
    private readonly UserManager userManager;
    private readonly ILogger? _logger;

    public UserRestController( UserManager userManager, ILoggerFactory loggerFactory )
    {
        this.userManager = userManager;
        //this.logger = loggerFactory.AddFile("logs/REST/UserRestControllerLog.txt").CreateLogger("UserRestController");
    }

    [HttpGet( "GetProfile" )]
    public ActionResult<User> GetProfile()
    {
        try
        {
            User u = new User();
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