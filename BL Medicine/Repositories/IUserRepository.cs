using BL_Medicine.Domain;

namespace BL_Medicine.Repositories;

public interface IUserRepository
{
    void UpdateUser();
    void Update();
    LoginResponse Login(string email, string password);
    LoginResponse Register(string firstname, string lastname, string email, string password, string confirmPassword);
}