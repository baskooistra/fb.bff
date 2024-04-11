using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
using Web.BFF.Domain.Configuration;
using CommunityToolkit.Diagnostics;
using Web.BFF.API.Endpoints.v1.Controllers;
using Web.BFF.API.Endpoints.v1.Models.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Web.BFF.API.Extensions;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.Configure<WebAppConfiguration>(
    builder.Configuration.GetSection(WebAppConfiguration.Key));

builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddFrontendCors();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});
builder.WebHost.UseKestrelHttpsConfiguration();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseCors("localhost");
app.MapConfigurationEndpoints();

app.Run();


[JsonSerializable(typeof(WebAppConfigurationResponse))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}