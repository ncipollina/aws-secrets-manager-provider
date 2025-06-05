using System;
using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AWSSecretsManager.Provider.Internal;

public class SecretsManagerConfigurationSource : IConfigurationSource
{
    public SecretsManagerConfigurationSource(AWSCredentials? credentials = null, SecretsManagerConfigurationProviderOptions? options = null)
    {
        Credentials = credentials;
        Options = options ?? new SecretsManagerConfigurationProviderOptions();
    }

    public SecretsManagerConfigurationProviderOptions Options { get; }

    public AWSCredentials? Credentials { get; }

    public RegionEndpoint? Region { get; set; }

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        var client = CreateClient();
        
        // Try to resolve logger from the builder's properties (if available)
        ILogger? logger = null;
        if (builder.Properties.TryGetValue("Services", out var servicesObj) && servicesObj is IServiceProvider services)
        {
            logger = services.GetService<ILogger<SecretsManagerConfigurationProvider>>();
        }

        return new SecretsManagerConfigurationProvider(client, Options, logger);
    }

    private IAmazonSecretsManager CreateClient()
    {
        if (Options.CreateClient != null)
        {
            return Options.CreateClient();
        }

        var clientConfig = new AmazonSecretsManagerConfig
        {
            RegionEndpoint = Region
        };

        Options.ConfigureSecretsManagerConfig(clientConfig);

        return Credentials switch
        {
            null => new AmazonSecretsManagerClient(clientConfig),
            _ => new AmazonSecretsManagerClient(Credentials, clientConfig)
        };
    }
}

/// <summary>
/// Configuration source that supports explicit logger injection
/// </summary>
public class SecretsManagerConfigurationSourceWithLogger : SecretsManagerConfigurationSource
{
    private readonly ILogger _logger;
    
    public SecretsManagerConfigurationSourceWithLogger(AWSCredentials? credentials, SecretsManagerConfigurationProviderOptions options, ILogger logger) 
        : base(credentials, options)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public new IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        var client = CreateClient();
        return new SecretsManagerConfigurationProvider(client, Options, _logger);
    }

    private IAmazonSecretsManager CreateClient()
    {
        if (Options.CreateClient != null)
        {
            return Options.CreateClient();
        }

        var clientConfig = new AmazonSecretsManagerConfig
        {
            RegionEndpoint = Region
        };

        Options.ConfigureSecretsManagerConfig(clientConfig);

        return Credentials switch
        {
            null => new AmazonSecretsManagerClient(clientConfig),
            _ => new AmazonSecretsManagerClient(Credentials, clientConfig)
        };
    }
}