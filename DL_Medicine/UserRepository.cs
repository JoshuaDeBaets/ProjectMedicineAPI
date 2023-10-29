using BL_Medicine.Domain;
using BL_Medicine.Repositories;

namespace DL_Medicine;

public class UserRepository : IUserRepository
{
    private string _connectionstring;
    public UserRepository( string connectionstring )
    {
        _connectionstring = connectionstring;
    }
    
    public void Register()
    {
        
    }
        
        
    public void UpdateUser()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }

    public User GetProfile()
    {
        throw new NotImplementedException();
    }
}