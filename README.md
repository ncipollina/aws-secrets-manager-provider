# AWSSecretsManager.Provider
[![NuGet version](https://img.shields.io/nuget/vpre/AWSSecretsManager.Provider.svg)](https://www.nuget.org/packages/AWSSecretsManager.Provider)
[![NuGet Downloads](https://img.shields.io/nuget/dt/AWSSecretsManager.Provider.svg)](https://www.nuget.org/packages/AWSSecretsManager.Provider/)
[![Build Status](https://github.com/LayeredCraft/aws-secrets-manager-provider/actions/workflows/build.yaml/badge.svg)](https://github.com/LayeredCraft/aws-secrets-manager-provider/actions)

This is a modern, community-maintained fork of [Kralizek/AWSSecretsManagerConfigurationExtensions](https://github.com/Kralizek/AWSSecretsManagerConfigurationExtensions), originally developed by Renato Golia.

It provides a configuration provider for [Microsoft.Extensions.Configuration](https://www.nuget.org/packages/Microsoft.Extensions.Configuration/) that loads secrets from [AWS Secrets Manager](https://aws.amazon.com/secrets-manager/).

---

## 🚀 What's New in This Fork

- ✅ Targeted to .NET 8 and .NET 9
- ✅ Converted to use `System.Text.Json` only
- ✅ Refactored structure for better modern SDK usage
- ✅ Published as a new NuGet package: [`AWSSecretsManager.Provider`](https://www.nuget.org/packages/AWSSecretsManager.Provider)

---

## 🔧 Usage

### ASP.NET Core Example

```csharp
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddSecretsManager(); // 👈 AWS Secrets Manager integration
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
```

### Console App Example

```csharp
static void Main(string[] args)
{
    var builder = new ConfigurationBuilder();
    builder.AddSecretsManager();

    var config = builder.Build();
    Console.WriteLine("Secret: " + config["MySecret"]);
}
```

Your application must have AWS credentials available through the default AWS SDK mechanisms. Learn more here:  
👉 [AWS SDK Credential Config](https://docs.aws.amazon.com/sdk-for-net/v3/developer-guide/net-dg-config-creds.html)

---

## 🔒 Configuration Options

This provider supports several customization options, including:

- **Credentials**: Pass your own credentials if needed.
- **Region**: Customize the AWS region.
- **Filtering**: Control which secrets are loaded via filters or explicit allow lists.
- **Key generation**: Customize how configuration keys are named.
- **Version stage**: Set version stages for secrets.
- **LocalStack support**: Override `ServiceUrl` for local testing.

Refer to [samples](/samples/) for examples of each option.

---

## 📦 Installation

```bash
dotnet add package Aws.SecretsManager.Provider
```

---

## ✅ Building Locally

This repo is built with the standard .NET SDK:

```bash
dotnet build
dotnet test
```

---

## 🙌 Acknowledgments

This project is based on the excellent work by [Renato Golia](https://github.com/Kralizek) and inspired by the broader .NET and AWS developer community.

---

## 📄 License

This project is licensed under the [MIT License](LICENSE).