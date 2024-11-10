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
using FinTrack.Application.Operation.Authorization;
using FinTrack.Application.Security.Authorization;
using FinTrack.Application.Utils;
using FinTrack.Web;
using FinTrack.Web.Filters;
using FluentValidation.AspNetCore;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAuthorization(opts =>
{
    opts.AddPolicy(
        SecurityAuthorization.ViewSecurityPolicy,
        policy => policy.Requirements.Add(new SecurityAuthorization.SameAuthorRequirement())
    );
    opts.AddPolicy(
        OperationAuthorization.ChangeOperationPolicy,
        policy => policy.Requirements.Add(new OperationAuthorization.SameAuthorRequirement()));
});
builder.Services.AddAuthentication()
    .AddBearerToken(IdentityConstants.BearerScheme);

builder.Services
    .AddIdentityCore<EFUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<FinTrackDbContext>()
    .AddApiEndpoints();

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add<LockoutAuthorizationActionPolicy>();
    opts.Filters.Add<PaginationHeaderActionFilter>();
    opts.Filters.Add<UserIdActionFilter>(order: int.MinValue);
    opts.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
    opts.Conventions.Add(new RouteTokenTransformerConvention(new PascalToKebabParameterTransformer()));
});

/*
 * For some reason some options when added through AddJsonOptions are not applied.
 * Duplicating the code for these two namespaces seems so solve the problems.
 * It seems that Microsoft.AspNetCore.Mvc.JsonOptions is being used when parsing requests and
 * Microsoft.AspNetCore.Http.Json.JsonOptions is used when serializing responses
 */
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(opts =>
{
    opts.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    opts.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
    opts.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.Configure<Microsoft.AspNetCore.Mvc.JsonOptions>(opts =>
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

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationRulesToSwagger();

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

app.Services.SaveSwaggerJson();

app.Run();
