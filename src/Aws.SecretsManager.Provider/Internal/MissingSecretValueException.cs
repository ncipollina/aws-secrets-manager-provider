using System;

namespace Aws.SecretsManager.Provider.Internal;

public class MissingSecretValueException : Exception
{
    public MissingSecretValueException(string errorMessage, string secretName, string secretArn, Exception exception) : base(errorMessage, exception)
    {
        SecretName = secretName;
        SecretArn = secretArn;
    }

    public string SecretArn { get; }

    public string SecretName { get; }
}