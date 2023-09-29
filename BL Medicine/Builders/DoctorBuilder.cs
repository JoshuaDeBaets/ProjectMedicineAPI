using BL_Medicine.Domain;
using BL_Medicine.Exceptions;

namespace BL_Medicine.Builders;

public class DoctorBuilder
{
    private readonly Doctor _doctor;
    
    
    public DoctorBuilder SetFirstName( string firstname )
    {
        if ( string.IsNullOrWhiteSpace( firstname ) )
        {
            throw new UserException( "firstname cannot be null");
        }
        _doctor.Firstname = firstname;
        return this;
    }

    public DoctorBuilder SetSurname( string surname )
    {
        _doctor.Surname = surname;
        return this;
    }

    public Doctor Build()
    {
        return _doctor;
    }
    
    
}