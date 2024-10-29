using FinTrack;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Text.Json;
using FinTrack.Application;
using FinTrack.Persistence;
using Microsoft.AspNetCore.Identity;
using FinTrack.Persistence.Models;
using FinTrack.Persistence.Contexts;
using System.Text.Json.Serialization;
using FinTrack.Web;
using FinTrack.Web.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthorization();
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services
    .AddIdentityCore<EFUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FinTrackDbContext>()
    .AddApiEndpoints();

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add<LockoutAuthorizationPolicy>();
    opts.Filters.Add<UserIdActionFilter>(order: int.MinValue);
    opts.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
    opts.Conventions.Add(new RouteTokenTransformerConvention(new PascalToKebabParameterTransformer()));
}).AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add other layer's services
builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    using var scope = app.Services.CreateScope();
    var services = scope.ServiceProvider;
    MigrationService.DeleteDatabase(app.Services).Wait();
    MigrationService.ApplyMigrationsAndSeed(services, "Resources").Wait();
}

app.UseAuthorization();

app.MapControllers();

//app.MapIdentityApi<EFUser>();

app.Run();
