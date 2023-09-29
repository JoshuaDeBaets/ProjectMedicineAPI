namespace BL_Medicine.Domain;

public class Medicine
{

    public int? Id;
    
    public string? Name;

    public string? Description;

    public string? Barcode;

    public List<string> UsedFor;

    public List<string> SideEffects;

    public Medicine()
    {
        UsedFor = new();
        SideEffects = new();
    }


}