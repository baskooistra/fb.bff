using Microsoft.Extensions.Options;
using Web.BFF.Domain.Configuration;

namespace Web.BFF.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddFrontendCors(this IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();
            using (var scope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var domains = scope.ServiceProvider.GetRequiredService<IOptions<WebAppConfiguration>>()?.Value?.FrontendDomains;
                if (domains?.Length == 0)
                    return services;

                services.AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                    {
                        policy
                            .WithOrigins(domains!)
                            .AllowCredentials()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });

                return services;
            }
        }
    }
}
