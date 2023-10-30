using BL_Medicine.Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IUserRestController
{
    ActionResult<User> GetProfile(string token);
    ActionResult<User> FuckJews();
}