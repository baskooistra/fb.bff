using CommunityToolkit.Diagnostics;
using Microsoft.Extensions.Options;
using Web.BFF.Domain.Configuration;
using Azure.Identity;

namespace Web.BFF.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        const string AppConfigurationEndpoint = "ConfigurationEndpoint";
        const string AppConfigurationKey = "ConfigurationKey";

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

        public static WebApplicationBuilder AddCloudHostedServices(this WebApplicationBuilder builder)
        {
            builder.Configuration.AddAzureAppConfiguration(options =>
            {
                var endpoint = builder.Configuration.GetValue<string>(AppConfigurationEndpoint);
                var configurationKey = builder.Configuration.GetValue<string>(AppConfigurationKey) + ":";
                var environmentName = builder.Environment.EnvironmentName;

                Guard.IsNotNullOrWhiteSpace(endpoint);
                var config = options.Connect(new Uri(endpoint), new ManagedIdentityCredential())
                    .Select($"{configurationKey}*", environmentName)
                    .TrimKeyPrefix(configurationKey);
            });

            builder.Services.AddApplicationInsightsTelemetry();

            return builder;
        }
    }
}
