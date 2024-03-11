using ConnectSdk.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConnectSdk.Extensions.AspNetCore.Configuration.Secrets;

public class KeyVaultSecretManager
{
    internal static KeyVaultSecretManager Instance { get; } = new();

    /// <summary>
    /// Maps secret to a configuration key.
    /// </summary>
    /// <param name="secret">The <see cref="KeyVaultSecret"/> instance.</param>
    /// <returns>Configuration key name to store secret value.</returns>
    public virtual string GetKey(FullItem secret)
    {
        return secret.Title.Replace("--", ConfigurationPath.KeyDelimiter);
    }

    /// <summary>
    /// Converts a loaded list of secrets into a corresponding set of configuration key-value pairs.
    /// </summary>
    /// <param name="secrets">A set of secrets retrieved during <see cref="AzureKeyVaultConfigurationProvider.Load"/> call.</param>
    /// <returns>The dictionary of configuration key-value pairs that would be assigned to the <see cref="ConfigurationProvider.Data"/>
    /// and exposed from the <see cref="IConfiguration"/>.</returns>
    /// <exception cref="ArgumentNullException">When <paramref name="secrets"/> is <code>null</code>.</exception>
    public virtual Dictionary<string, string> GetData(IEnumerable<FullItem> secrets)
    {
        if (secrets is null)
            throw new ArgumentException(nameof(secrets));

        var data = new Dictionary<string, FullItem>(StringComparer.OrdinalIgnoreCase);

        foreach (var secret in secrets)
        {
            string key = GetKey(secret);

            // It is possible that multiple
            // LoadedSecrets objects uses the same configuration key. This loop
            // takes the latest updated value for each key.
            if (data.TryGetValue(key, out FullItem currentSecret))
            {
                if (secret.UpdatedAt > currentSecret.UpdatedAt)
                {
                    data[key] = secret;
                }
            }
            else
            {
                data.Add(key, secret);
            }
        }

        return data.ToDictionary(d => d.Key, v => GetValue(v.Value), StringComparer.OrdinalIgnoreCase);
    }

    private string GetValue(FullItem item)
    {
        var password = item.Fields.FirstOrDefault(x => x.Purpose == FieldPurpose.PASSWORD);

        if (password is null)
            throw new MissingFieldException(nameof(password));

        return password.Value;
    }
}