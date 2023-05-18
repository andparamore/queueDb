using Microsoft.EntityFrameworkCore;
using QueueInfrastructure.Helpers;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Context;
using QueueInfrastructure.Models.Enum;
using QueueInfrastructure.Models.Models;

namespace QueueBuilder.Repositories;

public class QueueBuilderRepository : IQueueBuilderRepository
{
    private readonly IDbContextFactory<EntityContext> _ctxFactory;

    public QueueBuilderRepository(IDbContextFactory<EntityContext> ctxFactory)
    {
        _ctxFactory = ctxFactory.ThrowIfNull();
    }
    
    public async Task<RequestModel> GetRequestById(Guid id)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        return await ctx.RequestModels
            .AsNoTracking()
            .FirstAsync(x => x.Id == id);
    }

    public async Task AddRequest(RequestModel requestModel)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        ctx.Add(requestModel);
        await ctx.SaveChangesAsync();
    }
    
    public async Task RemoveRequest(RequestModel requestModel)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        ctx.Remove(requestModel);
        await ctx.SaveChangesAsync();
    }
    
    public async Task RemoveRangeRequest(IEnumerable<RequestModel> requestModels)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        ctx.RemoveRange(requestModels);
        await ctx.SaveChangesAsync();
    }
    
    public async Task UpdateStatusRequest(RequestModelView request, Status status)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        var requestModel = await ctx.RequestModels
            .AsNoTracking()
            .FirstAsync(x => x.Id == request.Id);
                
        ctx.Attach(requestModel);
        requestModel.Status = Status.Completed;
        await ctx.SaveChangesAsync();
    }

    public async Task AddRequestType(RequestTypeConfigurationModel requestTypeConfigurationModel)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        ctx.Add(requestTypeConfigurationModel);
        await ctx.SaveChangesAsync();
    }

    public async Task<RequestTypeConfigurationModel> GetWeightByType(RequestTypesEnum type)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();

        return await ctx.RequestTypeConfigurations
            .AsNoTracking()
            .Include(x => x.Steps)
            .FirstAsync(x => x.TypeName == type);
    }

    public Task<double> GetWeightMultiplierByType(RequestTypesEnum type)
    {
        throw new NotImplementedException();
    }

    public async Task<int> GetTotalStepByType(RequestTypesEnum type)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();

        return (await ctx.RequestTypeConfigurations
            .AsNoTracking()
            .Include(x => x.Steps)
            .FirstAsync(x => x.TypeName == type)).Steps.Count();
    }

    public async Task<IEnumerable<RequestTypeConfigurationModel>> GetTypesConfiguration()
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();

        return await ctx.RequestTypeConfigurations
            .AsNoTracking()
            .Include(x => x.Steps)
            .ToListAsync();
    }
}