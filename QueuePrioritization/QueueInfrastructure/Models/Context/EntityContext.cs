using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using QueueInfrastructure.Models.Config;
using QueueInfrastructure.Models.Models;

namespace QueueInfrastructure.Models.Context;

public sealed class EntityContext : DbContext
{
    public DbSet<RequestModel> RequestModels { get; set; }
    
    public DbSet<RequestTypeConfigurationModel> RequestTypeConfigurations { get; set; }
    
    public DbSet<StepConfigurationModel> StepConfigurations { get; set; }
    
    public DbSet<RequestModelView> RequestModelsView { get; set; }

    public EntityContext(DbContextOptions<EntityContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder
            .Entity<RequestTypeConfigurationModel>(SetPropertyRequestTypeConfigurations)
            .Entity<StepConfigurationModel>(SetPropertyStepConfigurations)
            .Entity<RequestModel>(SetPropertyRequestModels)
            .Entity<RequestModelView>(SetPropertyRequestModelsView);
    }
    
    private void SetPropertyRequestModels(EntityTypeBuilder<RequestModel> entity)
    {
        _ = entity.ToTable("request_model");
        
        _ = entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        
        _ = entity.Property(e => e.Payload)
            .HasMaxLength(50)
            .HasColumnName("payload");
        
        _ = entity.Property(e => e.Weight)
            .HasColumnName("weight");
        
        _ = entity.Property(e => e.CurrentStep)
            .HasColumnName("current_step");
        
        _ = entity.Property(e => e.Attempts)
            .HasColumnName("attempts");
        
        _ = entity.Property(e => e.Status)
            .HasColumnName("status");
        
        _ = entity.Property(e => e.RequestType)
            .HasColumnName("request_type");
        
        _ = entity.Property(e => e.InitialRequestId)
            .HasColumnName("initial_request_id");
        
        _ = entity.Property(e => e.PreviousRequestId)
            .HasColumnName("previous_request_id");
        
        _ = entity.Property(e => e.TimeStampArrived)
            .HasColumnName("time_stamp_arrived");
        //TODO
    }

    private void SetPropertyRequestTypeConfigurations(EntityTypeBuilder<RequestTypeConfigurationModel> entity)
    {
        _ = entity.ToTable("request_type_configurations");
        
        _ = entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        
        _ = entity.Property(e => e.TypeName)
            .HasMaxLength(50)
            .HasColumnName("type_name");
        
        _ = entity.Property(e => e.Weight)
            .HasColumnName("weight");
    }
    
    private void SetPropertyStepConfigurations(EntityTypeBuilder<StepConfigurationModel> entity)
    {
        _ = entity.ToTable("step_configurations");
        
        _ = entity.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        
        _ = entity.Property(e => e.WeightMultiplier)
            .HasColumnName("weight_multiplier");
        
        _ = entity.Property(e => e.StepNumber)
            .HasColumnName("step_number");

        _ = entity.HasOne(p => p.RequestTypeConfigurationModel)
            .WithMany(s => s.Steps)
            .HasForeignKey(p => p.RequestTypeId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("fk_request_type_steps");
    }

    private void SetPropertyRequestModelsView(EntityTypeBuilder<RequestModelView> entity)
    {
        _ = entity.HasNoKey();
        
        _ = entity.ToView("view_request_model");
        
        _ = entity.Property(e => e.Id)
            .HasColumnName("id");
        
        _ = entity.Property(e => e.Payload)
            .HasColumnName("payload");
        
        _ = entity.Property(e => e.Priority)
            .HasColumnName("priority");
        
        _ = entity.Property(e => e.RequestType)
            .HasColumnName("request_type");
        
        _ = entity.Property(e => e.Status)
            .HasColumnName("status");
        
        _ = entity.Property(e => e.CurrentStep)
            .HasColumnName("current_step");
        
        _ = entity.Property(e => e.InitialRequestId)
            .HasColumnName("initial_request_id");
    }
}