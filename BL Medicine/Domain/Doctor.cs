namespace BL_Medicine.Domain;

public class Doctor : Person
{

    public Address Address;   

    public string? PracticeName;

    public string? PhoneNumber;

    public string? Specialisation;

    public List<DateTime> Calender;

    public Doctor()
    {
        Calender = new();
    }
}