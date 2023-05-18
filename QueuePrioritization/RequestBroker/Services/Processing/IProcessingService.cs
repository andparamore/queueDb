using QueueInfrastructure.Models.Models;

namespace RequestBroker.Services.Processing;

public interface IProcessingService
{
    Task ProcessAsync();
}