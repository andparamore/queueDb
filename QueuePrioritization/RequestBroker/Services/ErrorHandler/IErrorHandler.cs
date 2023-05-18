using QueueInfrastructure.Models.Models;

namespace RequestBroker.Services.ErrorHandler;

public interface IErrorHandler
{
    Task ErrorHandling(RequestModelView requestModel);
}