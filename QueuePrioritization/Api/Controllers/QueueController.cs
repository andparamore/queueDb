using Microsoft.AspNetCore.Mvc;
using QueueApi.Services.ControllerHandler;
using QueueBuilder.Config;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Models;

namespace QueueApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QueueController : Controller
{
    [HttpGet("requests")]
    public async Task<List<RequestModel>> GetRequests([FromServices] IControllerHandler handler) =>
        (await handler.GetRequests()).ToList();
    
    [HttpGet("pending_requests")]
    public async Task<List<RequestModelView>> GetPendingRequests([FromServices] IControllerHandler handler) =>
        (await handler.GetPendingRequests()).ToList();


    [HttpPost("requests")]
    public async Task AddRequest(
        [FromServices] IControllerHandler handler,
        [FromBody] RequestDto dto) => 
        await handler.AddRequest(dto);

    [HttpPut("requests")]
    public async Task UpdatePriorityRequest(
        [FromServices] IControllerHandler handler,
        [FromBody] WeightUpdateDto dto) =>
        await handler.UpdatePriorityRequest(dto);
    
    [HttpGet("process")]
    public async Task HandleRequest([FromServices] IControllerHandler handler) =>
        await handler.Handle();
    
    [HttpGet("process/{minutes}")]
    public async Task<int> HandleRequests([FromServices] IControllerHandler handler, int minutes) =>
        await handler.HandlingMultipleRequests(minutes);
    
    [HttpGet("parallel_process/{loopsCount}")]
    public async Task ParallelHandleRequests([FromServices] IControllerHandler handler, int loopsCount) =>
        await handler.ParallelHandlingMultipleRequests(loopsCount);
    
    [HttpGet("types")]
    public async Task<List<RequestTypeConfiguration>> GetTypesConfiguration([FromServices] IControllerHandler handler) =>
        (await handler.GetTypesConfiguration()).ToList();
    
    [HttpPost("types")]
    public async Task AddTypes(
        [FromServices] IControllerHandler handler,
        [FromBody] RequestTypeConfigurationModel requestTypeConfigurationModel) => 
        await handler.AddRequestType(requestTypeConfigurationModel);

}