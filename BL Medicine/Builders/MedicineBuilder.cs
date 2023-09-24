using BL_Medicine.Domain;
using BL_Medicine.Exceptions;

namespace BL_Medicine.Builders;

public class MedicineBuilder
{
    private readonly Medicine _medicine = new( );

    public MedicineBuilder SetId( int? id )
    {
        if ( id.HasValue )
        {
            _medicine.Id = id;
        }
        return this;
    }

    public MedicineBuilder SetName(string name)
    {
        if ( string.IsNullOrEmpty( name ) )
        {
            throw new MedicineException( "Please fill in the name of the medicine" );
        }
        _medicine.Name = name;
        return this;
    }

    public MedicineBuilder SetDescription(string description)
    {
        if ( string.IsNullOrEmpty( description ) )
        {
            throw new MedicineException( "Please fill in the description" );
        }
        _medicine.Description = description;
        return this;
    }

    public MedicineBuilder SetBarcode(string barcode)
    {
        //check for possible requirements.
        _medicine.Barcode = barcode;
        return this;
    }

    public MedicineBuilder AddUsedFor(string usedFor)
    {
        
        _medicine.UsedFor.Add(usedFor);
        return this;
    }

    public MedicineBuilder AddSideEffect(string sideEffect)
    {
        _medicine.SideEffects.Add(sideEffect);
        return this;
    }

    public Medicine Build()
    {
        // additional checks before build? (an adult can not weigth 20kg for example)
        return _medicine;
    }

}

