using FinTrack;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using System.Text.Json;
using FinTrack.Application;
using FinTrack.Persistence;
using Microsoft.AspNetCore.Identity;
using FinTrack.Persistence.Models;
using FinTrack.Persistence.Contexts;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigurePersistenceApp(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.AddAuthorization(opts =>
{
    opts.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
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
    opts.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider());
    opts.Conventions.Add(new RouteTokenTransformerConvention(new PascalToKebabParameterTransformer()));
}).AddJsonOptions(opts =>
{
    opts.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

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

app.UseAuthorization();

app.MapControllers();

app.Run();
