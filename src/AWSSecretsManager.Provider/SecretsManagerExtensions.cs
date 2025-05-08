using System;
using Amazon;
using Amazon.Runtime;
using AWSSecretsManager.Provider.Internal;
using Microsoft.Extensions.Configuration;

namespace AWSSecretsManager.Provider;

public static class SecretsManagerExtensions
{
    public static IConfigurationBuilder AddSecretsManager(this IConfigurationBuilder configurationBuilder,
        AWSCredentials? credentials = null,
        RegionEndpoint? region = null,
        Action<SecretsManagerConfigurationProviderOptions>? configurator = null)
    {
        var options = new SecretsManagerConfigurationProviderOptions();

        configurator?.Invoke(options);

        var source = new SecretsManagerConfigurationSource(credentials, options);

        if (region is not null)
        {
            source.Region = region;
        }

        configurationBuilder.Add(source);

        return configurationBuilder;
    }
}