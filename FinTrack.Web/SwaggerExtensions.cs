using Microsoft.OpenApi.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace FinTrack.Web;

public static class SwaggerExtensions
{
    public static void SaveSwaggerJson(this IServiceProvider provider)
    {
        var sw = provider.GetRequiredService<ISwaggerProvider>();
        var doc = sw.GetSwagger("v1", null, "/");
        var swaggerFile = doc.SerializeAsYaml(Microsoft.OpenApi.OpenApiSpecVersion.OpenApi3_0);
        File.WriteAllText("../docs/swaggerfile.yml", swaggerFile);
    }
}
