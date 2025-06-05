using System;
using Amazon;
using Amazon.Runtime;
using AWSSecretsManager.Provider.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

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

    /// <summary>
    /// Adds AWS Secrets Manager as a configuration source with explicit logger support.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder</param>
    /// <param name="logger">Logger instance for diagnostic information</param>
    /// <param name="credentials">AWS credentials</param>
    /// <param name="region">AWS region</param>
    /// <param name="configurator">Options configurator</param>
    /// <returns>The configuration builder</returns>
    public static IConfigurationBuilder AddSecretsManager(this IConfigurationBuilder configurationBuilder,
        ILogger<SecretsManagerConfigurationProvider> logger,
        AWSCredentials? credentials = null,
        RegionEndpoint? region = null,
        Action<SecretsManagerConfigurationProviderOptions>? configurator = null)
    {
        var options = new SecretsManagerConfigurationProviderOptions();

        configurator?.Invoke(options);

        var source = new SecretsManagerConfigurationSourceWithLogger(credentials, options, logger);

        if (region is not null)
        {
            source.Region = region;
        }

        configurationBuilder.Add(source);

        return configurationBuilder;
    }

    /// <summary>
    /// Adds AWS Secrets Manager as a configuration source with logger factory support.
    /// </summary>
    /// <param name="configurationBuilder">The configuration builder</param>
    /// <param name="loggerFactory">Logger factory for creating logger instances</param>
    /// <param name="credentials">AWS credentials</param>
    /// <param name="region">AWS region</param>
    /// <param name="configurator">Options configurator</param>
    /// <returns>The configuration builder</returns>
    public static IConfigurationBuilder AddSecretsManager(this IConfigurationBuilder configurationBuilder,
        ILoggerFactory loggerFactory,
        AWSCredentials? credentials = null,
        RegionEndpoint? region = null,
        Action<SecretsManagerConfigurationProviderOptions>? configurator = null)
    {
        var logger = loggerFactory.CreateLogger<SecretsManagerConfigurationProvider>();
        return configurationBuilder.AddSecretsManager(logger, credentials, region, configurator);
    }
}