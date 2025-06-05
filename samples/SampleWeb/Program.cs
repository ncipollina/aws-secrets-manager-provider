using AWSSecretsManager.Provider;

var builder = WebApplication.CreateBuilder(args);

// Example 1: Using logger factory from the WebApplicationBuilder (recommended approach)
using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
builder.Configuration.AddSecretsManager(
    loggerFactory,
    configurator: options => options.PollingInterval = TimeSpan.FromSeconds(10));

// Example 2: Alternative approach using explicit logger
// var logger = loggerFactory.CreateLogger<AWSSecretsManager.Provider.Internal.SecretsManagerConfigurationProvider>();
// builder.Configuration.AddSecretsManager(
//     logger,
//     configurator: options => options.PollingInterval = TimeSpan.FromSeconds(10));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/config", (IConfiguration config) => 
{
    // Display some configuration keys (be careful not to expose secrets in production!)
    var keys = config.AsEnumerable().Select(kvp => kvp.Key).ToArray();
    return new { ConfigurationKeys = keys, Count = keys.Length };
});

app.Run();