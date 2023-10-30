
namespace BL_Medicine.Domain;

public class User : Person
{
    public Enum? Gender { get; set; }

    public int? Weight { get; set; }

    public int? Height { get; set; }

    public bool IsEmailValidated { get; set; }

    public List<UserMedicine> UserMedicines { get; set; }

    public User()
    {
        UserMedicines = new();
    }

}