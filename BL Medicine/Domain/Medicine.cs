namespace BL_Medicine.Domain;

public class Medicine
{
    private int? _id;

    public int? Id
    {
        set
        {
            _id = value;
        }
    }
    
    public string Name;

    public string Description;

    public string Barcode;

    public List<string> UsedFor;

    public List<string> SideEffects;

    public Medicine()
    {
        UsedFor = new();
        SideEffects = new();
        
    }


}