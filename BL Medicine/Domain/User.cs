
namespace BL_Medicine.Domain;

public class User : Person
{
    public Enum? Gender;

    public int? Weight;

    public int? Height;

    public bool IsEmailValidated;
    
    public List<UserMedicine> UserMedicines;

    public User()
    {
        UserMedicines = new();
    }

}