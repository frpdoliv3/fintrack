using FinTrack.Server.Data;
using FinTrack.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using FinTrack.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DevelopmentConnection")
    ));
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<UserIdentity>()
    .AddEntityFrameworkStores<AppDbContext>();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

var accountGroup = app.MapGroup("account");
accountGroup.MapIdentityApi<UserIdentity>();

accountGroup.MapPost("logout", async (SignInManager<UserIdentity> signInManager) => 
{
    await signInManager.SignOutAsync();
    return Results.Ok();
}).RequireAuthorization();

accountGroup.MapGet("pingAuth", () => TypedResults.Empty).RequireAuthorization();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
