namespace QueuePrioritizationConsoleUpdate;

public class RequestTypeModel
{
    public int Id { get; init; }

    public string RequestTypeName { get; set; } = string.Empty;
    
    public int Weight { get; set; }

    /*
    public AccountGroupModel? AccountGroup { get; set; }
    ...
    */
}