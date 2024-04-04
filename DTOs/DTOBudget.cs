namespace ConvertDoc.DTOs;

public class DTOBudget
{
    public DateTime date { get; set; }
    public string name { get; set; }
    public DTOItem[] items { get; set; }

}