namespace ConvertDoc.DTOs;

public class DTOAgreement
{
    public string number { get; set; }
    public DateTime date { get; set; }
    public DTOEntity contracting { get; set; }
    public DTOItem[] items { get; set; }


}
