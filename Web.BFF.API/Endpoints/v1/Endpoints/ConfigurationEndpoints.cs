using AutoMapper;
using Microsoft.Extensions.Options;
using Web.BFF.API.Endpoints.v1.Models.Configuration;
using Web.BFF.Domain.Configuration;

namespace Web.BFF.API.Endpoints.v1.Controllers;

public static class ConfigurationEndpoints
{
    public static void MapConfigurationEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/configuration").WithTags(WebAppConfiguration.Key);

        group.MapGet("/", (IOptions<WebAppConfiguration> configuration, IMapper mapper) =>
        {
        return mapper.Map<WebAppConfigurationResponse>(configuration.Value);
        })
        .Produces<WebAppConfigurationResponse>()
        .WithName("GetWebAppConfiguration")
        .WithDescription("Fetches the frontend configurationfor web app from backend services")
        .WithOpenApi();
    }
}
