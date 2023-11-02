using BL_Medicine.Domain;
using Microsoft.AspNetCore.Mvc;

namespace API.Interfaces;

public interface IUserRestController
{
    ActionResult<User> GetProfile(string token);
    ActionResult<LoginResponse> Login(string email, string password);
    ActionResult<LoginResponse> Register(string firstname, string lastname, string email, string password, string confirmPassword);
}