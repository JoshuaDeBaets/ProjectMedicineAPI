namespace BL_Medicine.Domain;

public class Doctor : Person
{

    public Address Address { get; set; }

    public string? PracticeName { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Specialisation { get; set; }

    public List<DateTime> Calender { get; set; }

    public Doctor()
    {
        Calender = new();
    }
}