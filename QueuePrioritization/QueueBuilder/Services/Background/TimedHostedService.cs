using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using QueueBuilder.Services.QueueHandler;
using QueueInfrastructure.Models.Enum;
using QueueInfrastructure.Models.Models;

namespace QueueBuilder.Services.Background;

public class TimedHostedService : IHostedService, IDisposable
{
    private int _executionCount = 0;
    private IServiceProvider _services { get; }
    private readonly ILogger<TimedHostedService> _logger;
    private Timer? _timer;

    public TimedHostedService(ILogger<TimedHostedService> logger, IServiceProvider services)
    {
        _logger = logger;
        _services = services;
    }

    public async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service running.");
        
        _timer = new Timer(DoWork, null, new TimeSpan(0,0,10),
            TimeSpan.FromMilliseconds(100));
    }

    private async void DoWork(object? state)
    {
        var count = Interlocked.Increment(ref _executionCount);

        var random = new Random();
        random.Next(3);

        var request = new RequestDto
        {
            Payload = $"{count}",
            CurrentStep = 1,
            RequestType = RandomEnum(random.Next(3))
        };

        using var scope = _services.CreateScope();
        var builderHandler = scope.ServiceProvider.GetRequiredService<IQueueBuilderHandler>();
        await builderHandler.AddRequest(request);
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private RequestTypesEnum RandomEnum(int randomInt) => randomInt switch
    {
        1 => RequestTypesEnum.CreatingDocuments,
        2 => RequestTypesEnum.SignatureOfDocuments,
        3 => RequestTypesEnum.LocationRequest,
        _ => RequestTypesEnum.LocationRequest
    };
}