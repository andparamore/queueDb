using Microsoft.EntityFrameworkCore;
using QueueApi.QueueProcessing;
using QueueApi.Repositories;
using QueueApi.Services;
using QueueApi.Services.ControllerHandler;
using QueueBuilder.Repositories;
using QueueBuilder.Services.Background;
using QueueBuilder.Services.ConfigurationHandler;
using QueueBuilder.Services.QueueHandler;
using QueueBuilder.Services.WeightDistribution;
using QueueInfrastructure.Models.Context;
using RequestBroker.Services.ErrorHandler;
using RequestBroker.Services.Processing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Services.AddDbContextFactory<EntityContext>(c =>
    { 
        c.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddScoped<IQueueApiRepository, QueueApiRepository>();
builder.Services.AddScoped<IQueueBuilderRepository, QueueBuilderRepository>();
builder.Services.AddScoped<IConfigurationHandler, ConfigurationHandler>();
builder.Services.AddScoped<IWeightDistributionService, WeightDistributionService>();
builder.Services.AddHostedService<TimedHostedService>();
builder.Services.AddScoped<IErrorHandler, ErrorHandler>();
builder.Services.AddScoped<IQueueBuilderHandler, QueueBuilderHandler>();
builder.Services.AddScoped<IQueueApiHandler, QueueApiHandler>();
builder.Services.AddScoped<IProcessingService, ProcessingService>();
builder.Services.AddScoped<IControllerHandler, ControllerHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();