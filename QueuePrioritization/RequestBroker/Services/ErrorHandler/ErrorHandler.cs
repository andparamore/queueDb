using QueueBuilder.Repositories;
using QueueInfrastructure.Models.Models;

namespace RequestBroker.Services.ErrorHandler;

public class ErrorHandler : IErrorHandler
{
    private IQueueBuilderRepository _repository;
    
    public ErrorHandler(IQueueBuilderRepository repository)
    {
        _repository = repository;
    }
    
    public async Task ErrorHandling(RequestModelView requestModel)
    {
        
    }
}