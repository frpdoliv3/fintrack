using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace FinTrack.Application.Operation.Authorization;

public static class OperationAuthorizationExtensions
{
    public static void AddOperationAuthorization(this IServiceCollection services)
    {
        services
            .AddScoped<IAuthorizationHandler, 
                OperationAuthorizationHandler<OperationAuthorization.ChangeOperationRequirement>>();
    }
}