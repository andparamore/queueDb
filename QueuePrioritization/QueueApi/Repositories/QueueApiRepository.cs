using Microsoft.EntityFrameworkCore;
using QueueInfrastructure.Helpers;
using QueueInfrastructure.Models.Context;
using QueueInfrastructure.Models.Models;

namespace QueueApi.Repositories;

public class QueueApiRepository : IQueueApiRepository
{
    private readonly IDbContextFactory<EntityContext> _ctxFactory;

    public QueueApiRepository(IDbContextFactory<EntityContext> ctxFactory)
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

    public async Task<IEnumerable<RequestModel>> GetRequests()
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        return await ctx.RequestModels
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<RequestModel>> GetRequestsFromChain()
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        return await ctx.RequestModels
            .AsNoTracking()
            .Where(x => x.CurrentStep > 1)
            .ToListAsync();
    }

    public async Task<RequestModelView> GetPriorityRequest()
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        await using var transaction = await ctx.Database.BeginTransactionAsync();

        try
        {
            var request = await ctx.RequestModelsView
                .Where(x => x.Status == Status.Pending)
                .OrderByDescending(x => x.Priority)
                .FirstAsync();

            Console.WriteLine(request.Priority);
            
            try
            {
                var requestModel = await ctx.RequestModels
                    .AsNoTracking()
                    .FirstAsync(x => x.Id == request.Id);
                
                ctx.Attach(requestModel);
                requestModel.Status = Status.InProcessing;
                request.Status = Status.InProcessing;
                await ctx.SaveChangesAsync();

                await transaction.CommitAsync();
        
                return request;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateRequest(RequestModel requestModel)
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();
        
        ctx.Update(requestModel);
        await ctx.SaveChangesAsync();
    }

    public async Task ExpiryDateCheck()
    {
        await using var ctx = await _ctxFactory.CreateDbContextAsync();

        var timestamp = UnixTimestampHelper.GetUnixTimestampNow();
        var r = await ctx.RequestModels.AsNoTracking().Where(x => timestamp - x.TimeStampArrived > 850 && x.Status == Status.Pending ).ToListAsync();
        if (r.Count != 0)
        {
            throw new InvalidOperationException("Слишком долгое ожидание");
        }
    }
}