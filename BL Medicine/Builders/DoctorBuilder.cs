using BL_Medicine.Domain;
using BL_Medicine.Exceptions;
using BL_Medicine.RegexChecks;

namespace BL_Medicine.Builders;

public class DoctorBuilder
{
    private readonly Doctor _doctor;

    // Name of the Group Practice/Surgery you attend
    public DoctorBuilder SetPracticeName( string practiceName )
    {
        if (practiceName.isNull() )
        {
            throw new UserException("Surgery cannot be null");
        }
        _doctor.PracticeName = practiceName;
        return this;
    }

    public DoctorBuilder SetFirstname( string firstname )
    {
        _doctor.Firstname = firstname;
        return this;
    }

    public DoctorBuilder SetSurname( string surname )
    {
        _doctor.Surname = surname;
        return this;
    }

    public DoctorBuilder SetAddress( Address address )
    {
        _doctor.Address = address;
        return this;
    }

// this is the Phone of the Doctors that you would attend ie the Reception
    public DoctorBuilder SetNumber( string phonenumber )
    {
        if (phonenumber.isNull() )
        {
            throw new UserException("Invalid Number or No Number Given");
        }
        _doctor.PhoneNumber = phonenumber;
        return this;
    }
    public Doctor Build()
    {
        return _doctor;
    }
    
    
}