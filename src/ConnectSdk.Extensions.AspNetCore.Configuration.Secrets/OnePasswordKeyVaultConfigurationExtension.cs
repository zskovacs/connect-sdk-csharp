using Microsoft.Extensions.Configuration;
using System;

namespace ConnectSdk.Extensions.AspNetCore.Configuration.Secrets;

public static class OnePasswordKeyVaultConfigurationExtension
{
    public static IConfigurationBuilder AddOnePasswordKeyVault(
        this IConfigurationBuilder builder,
        Action<OnePasswordKeyVaultConfigurationOptions> options)
    {
        if (builder is null)
            throw new ArgumentException(nameof(builder));
        
        if (options is null)
            throw new ArgumentException(nameof(options));

        var empty = new OnePasswordKeyVaultConfigurationOptions();
        options(empty);

        return AddOnePasswordKeyVault(builder, empty);
    }
    
    public static IConfigurationBuilder AddOnePasswordKeyVault(
        this IConfigurationBuilder builder,
        OnePasswordKeyVaultConfigurationOptions options)
    {
        if (builder is null)
            throw new ArgumentException(nameof(builder));
        
        if (options is null)
            throw new ArgumentException(nameof(options));
        
        if (string.IsNullOrWhiteSpace(options.ApiKey))
            throw new ArgumentException($"{nameof(options)}.{nameof(options.ApiKey)}");
        
        if (string.IsNullOrWhiteSpace(options.BaseUrl))
            throw new ArgumentException($"{nameof(options)}.{nameof(options.BaseUrl)}");
        
        if (string.IsNullOrWhiteSpace(options.VaultId))
            throw new ArgumentException($"{nameof(options)}.{nameof(options.VaultId)}");
        
        var client = new OnePasswordConnectClient(options);

        return builder.AddOnePasswordKeyVault(client, options);
    }
    
    public static IConfigurationBuilder AddOnePasswordKeyVault(
        this IConfigurationBuilder builder,
        IOnePasswordConnectClient client,
        OnePasswordKeyVaultConfigurationOptions options)
    {
        if (builder is null)
            throw new ArgumentException(nameof(builder));
        
        if (options is null)
            throw new ArgumentException(nameof(options));
        
        if (client is null)
            throw new ArgumentException(nameof(client));
        
        builder.Add(new OnePasswordKeyVaultConfigurationSource(client, options));

        return builder;
    }
}