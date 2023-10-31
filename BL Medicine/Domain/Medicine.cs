namespace BL_Medicine.Domain;

public class Medicine
{

    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? Barcode { get; set; }

    public List<string> UsedFor { get; set; }

    public List<string> SideEffects { get; set; }

    public Medicine()
    {
        UsedFor = new();
        SideEffects = new();
    }


}