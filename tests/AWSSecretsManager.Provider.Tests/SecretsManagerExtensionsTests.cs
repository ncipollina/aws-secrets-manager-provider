using System;
using Amazon;
using Amazon.Runtime;
using AWSSecretsManager.Provider.Internal;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;
using AwesomeAssertions;

namespace AWSSecretsManager.Provider.Tests;

public class SecretsManagerExtensionsTests
{
    private readonly IConfigurationBuilder configurationBuilder;

    public SecretsManagerExtensionsTests()
    {
        configurationBuilder = Substitute.For<IConfigurationBuilder>();
    }

    [Fact]
    public void SecretsManagerConfigurationSource_can_be_added_via_convenience_method_with_no_parameters()
    {
        configurationBuilder.Add(Arg.Any<IConfigurationSource>()).Returns(configurationBuilder);

        SecretsManagerExtensions.AddSecretsManager(configurationBuilder);

        configurationBuilder.Received(1).Add(Arg.Any<SecretsManagerConfigurationSource>());
    }

    [Theory, CustomAutoData]
    public void SecretsManagerConfigurationSource_can_be_added_via_convenience_method_with_region(RegionEndpoint region)
    {
        configurationBuilder.Add(Arg.Any<IConfigurationSource>()).Returns(configurationBuilder);

        SecretsManagerExtensions.AddSecretsManager(configurationBuilder, region: region);

        configurationBuilder.Received(1).Add(Arg.Is<SecretsManagerConfigurationSource>(s => s.Region == region));
    }

    [Theory, CustomAutoData]
    public void SecretsManagerConfigurationSource_can_be_added_via_convenience_method_with_optionConfigurator(Action<SecretsManagerConfigurationProviderOptions> optionConfigurator)
    {
        configurationBuilder.Add(Arg.Any<IConfigurationSource>()).Returns(configurationBuilder);

        SecretsManagerExtensions.AddSecretsManager(configurationBuilder, configurator: optionConfigurator);

        configurationBuilder.Received(1).Add(Arg.Any<SecretsManagerConfigurationSource>());

        optionConfigurator.Received(1)(Arg.Any<SecretsManagerConfigurationProviderOptions>());
    }

    [Theory, CustomAutoData]
    public void SecretsManagerConfigurationSource_can_be_added_via_convenience_method_with_credentials(AWSCredentials credentials)
    {
        configurationBuilder.Add(Arg.Any<IConfigurationSource>()).Returns(configurationBuilder);

        SecretsManagerExtensions.AddSecretsManager(configurationBuilder, credentials);

        configurationBuilder.Received(1).Add(Arg.Any<SecretsManagerConfigurationSource>());
    }

    [Theory, CustomAutoData]
    public void SecretsManagerConfigurationSource_can_be_added_via_convenience_method_with_credentials_and_region(AWSCredentials credentials, RegionEndpoint region)
    {
        configurationBuilder.Add(Arg.Any<IConfigurationSource>()).Returns(configurationBuilder);

        SecretsManagerExtensions.AddSecretsManager(configurationBuilder, credentials, region);

        configurationBuilder.Received(1).Add(Arg.Is<SecretsManagerConfigurationSource>(s => s.Region == region));
    }
}