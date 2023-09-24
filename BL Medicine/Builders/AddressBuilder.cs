using BL_Medicine.Domain;

namespace BL_Medicine.Builders;

public class AddressBuilder
{
    private readonly Address _address = new();
    

    public AddressBuilder SetStreet( string street )
    {
        _address.street = street;
        return this;
    }

    public AddressBuilder SetNumber( string number )
    {
        _address.number = number;
        return this;
    }

    public AddressBuilder SetBus( string bus )
    {
        _address.bus = bus;
        return this;
    }

    public AddressBuilder SetPostalCode( string postalcode )
    {
        _address.postalcode = postalcode;
        return this;
    }

    public AddressBuilder SetPlace( string place )
    {
        _address.place = place;
        return this;
    }

    public AddressBuilder SetCountry( string country )
    {
        _address.country = country;
        return this;
    }

    public Address Build()
    {
        return _address;
    }
}
