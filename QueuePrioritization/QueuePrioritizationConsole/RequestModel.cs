namespace QueuePrioritizationConsole;

public class RequestModel
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public int RequestTypeId { get; set; }
    
    public int Priority { get; set; }
    
    public DateTime CreateDate { get; set; } = DateTime.UtcNow;
    
    
}