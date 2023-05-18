namespace QueuePrioritizationConsole;

public class RequestTypeModel
{
    public int Id { get; init; }

    public string RequestTypeName { get; set; } = string.Empty;
    
    public int Weight { get; set; }
    
    public int TotalWeight { get; set; }
    
    /*
    public AccountGroupModel? AccountGroup { get; set; }
    ...
    */
}