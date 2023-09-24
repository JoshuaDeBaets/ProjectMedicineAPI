namespace BL_Medicine.Domain;

public class Doctor : Person
{
    public Address Adress;

    public string Specialisation;

    public List<DateTime> Calender;

    public Doctor()
    {
        Calender = new();
    }
}