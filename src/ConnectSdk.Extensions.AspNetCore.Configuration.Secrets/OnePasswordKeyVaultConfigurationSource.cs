using Microsoft.Extensions.Configuration;

namespace ConnectSdk.Extensions.AspNetCore.Configuration.Secrets;

/// <summary>
/// Represents One Password Key Vault secrets as an <see cref="IConfigurationSource"/>.
/// </summary>
public class OnePasswordKeyVaultConfigurationSource : IConfigurationSource
{
    private readonly OnePasswordKeyVaultConfigurationOptions _options;
    private readonly IOnePasswordConnectClient _client;
    
    /// <summary>
    /// Creates a new instance of <see cref="OnePasswordKeyVaultConfigurationSource"/>.
    /// </summary>
    /// <param name="client">The <see cref="IOnePasswordConnectClient"/> to use for retrieving values.</param>
    /// <param name="options">The <see cref="OnePasswordKeyVaultConfigurationOptions"/> to configure provider behaviors.</param>
    public OnePasswordKeyVaultConfigurationSource(IOnePasswordConnectClient client, OnePasswordKeyVaultConfigurationOptions options)
    {
        _client = client;
        _options = options;
    }

    /// <inheritdoc />
    public IConfigurationProvider Build(IConfigurationBuilder builder) => new OnePasswordKeyVaultConfigurationProvider(_client, _options);
}